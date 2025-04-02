using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.InterfaceServices;
using Core.DomainModels;
using Infrastructure.Context;
using Infrastructure.Interface;
using Infrastructure.Repository;

namespace Application.Services
{
    public class ProposalPaymentFlowSummaryService : IProposalPaymentFlowSummaryService
    {
        private readonly BaseContextGlobalhit _context;
        private readonly IProposalRepository _proposalRepository;
        private readonly IPaymentFlowSummaryRepository _summaryRepository;
        private readonly IPaymentScheduleRepository _scheduleRepository;
        private readonly IPaymentFlowSummaryService _summaryService;

        public ProposalPaymentFlowSummaryService(BaseContextGlobalhit context, IProposalRepository proposalRepository, IPaymentFlowSummaryRepository summaryRepository, IPaymentScheduleRepository scheduleRepository, IPaymentFlowSummaryService summaryService)
        {
            _context = context;
            _proposalRepository = proposalRepository;
            _summaryRepository = summaryRepository;
            _scheduleRepository = scheduleRepository;
            _summaryService = summaryService;
        }

        
    }
}
