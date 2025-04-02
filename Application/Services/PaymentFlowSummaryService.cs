using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.InterfaceServices;
using Core.DomainModels;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PaymentFlowSummaryService : IPaymentFlowSummaryService
    {
        private readonly BaseContextGlobalhit _context;
        private readonly IProposalRepository _proposalRepository;
        private readonly IPaymentFlowSummaryRepository _summaryRepository;
        private readonly IPaymentScheduleRepository _scheduleRepository;

        public PaymentFlowSummaryService(BaseContextGlobalhit context, IProposalRepository proposalRepository, IPaymentFlowSummaryRepository summaryRepository, IPaymentScheduleRepository scheduleRepository)
        {
            _context = context;
            _proposalRepository = proposalRepository;
            _summaryRepository = summaryRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<PaymentFlowSummary> ReturnFlowSummaryCalculated(int flowSummaryId)
        {
            var summary = await _context.paymentFlowSummaries.Include(u => u.PaymentSchedules).Where(x => x.Id == flowSummaryId).FirstOrDefaultAsync();

            if (summary == null)
                return null;

            return summary;
        }

        public async Task<PaymentFlowSummary> CreateandCalculateFlowSummary(Proposal proposal)
        {
            double monthlyRate = CalculateMonthRate(proposal);

            double monthPayment = CalculateMonthPayment(proposal, monthlyRate);

            double totalPaid = CalculateTotalPaid(proposal, monthPayment);

            double totalInterest = CalculateTotalInterest(proposal, totalPaid);

            var summary = new PaymentFlowSummary()
            {
                MonthlyPayment = monthlyRate,
                TotalInterest = totalInterest,
                TotalPayment = totalPaid

            };

            var paymentSummarysave = await _summaryRepository.AddAsync(summary);


            var lastValue = proposal.LoanAmount;
            for (int i = 0; i < proposal.NumberOfMonths; i++)
            {

                var baseInterest = lastValue * monthlyRate; 
                var principal = monthPayment - baseInterest;
                lastValue -= principal; 

                var schedule = new PaymentSchedule()
                {
                    Interest = Math.Round(baseInterest, 2), 
                    Balance = Math.Round(lastValue, 2), 
                    Principal = Math.Round(principal, 2), 
                    PaymentSummaryId = paymentSummarysave.Id
                };

                var scheduleSaved = await _scheduleRepository.AddAsync(schedule);
            }

            return await ReturnFlowSummaryCalculated(paymentSummarysave.Id);
        }

        private static double CalculateTotalInterest(Proposal proposal, double totalPaid)
        {
            // Calcula o total de juros pagos
            return Math.Round(totalPaid - proposal.LoanAmount, 2);
        }

        private static double CalculateMonthRate(Proposal proposal)
        {
            // Calcula a taxa mensal: taxa anual dividida por 12
            return proposal.AnnualInterestRate / 12;
        }

        private static double CalculateTotalPaid(Proposal proposal, double monthPayment)
        {
            // Calcula o total pago ao longo do prazo
            return Math.Round(monthPayment * proposal.NumberOfMonths, 2);
        }

        private static double CalculateMonthPayment(Proposal proposal, double monthlyRate)
        {
            // Implementa a fórmula da Tabela Price para calcular o pagamento mensal
            double numerator = monthlyRate * (double)Math.Pow((double)(1 + monthlyRate), proposal.NumberOfMonths);
            double denominator = (double)Math.Pow((double)(1 + monthlyRate), proposal.NumberOfMonths) - 1;

            // Calcula o valor mensal
            double monthlyPayment = proposal.LoanAmount * (numerator / denominator);

            // Arredonda para duas casas decimais
            return Math.Round(monthlyPayment, 2);
        }
    }
}
