using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.InterfaceServices;
using Core.DomainModels;
using Infrastructure.Context;
using Infrastructure.Interface;

namespace Application.Services
{
    public class ProposalService : IProposalService
    {
        private readonly BaseContextGlobalhit _context;
        private readonly IProposalRepository _proposalRepository;
        private readonly IPaymentFlowSummaryRepository _summaryRepository;
        private readonly IPaymentScheduleRepository _scheduleRepository;
        private readonly IPaymentFlowSummaryService _summaryService;

        public ProposalService(BaseContextGlobalhit context, IProposalRepository proposalRepository, IPaymentFlowSummaryRepository summaryRepository, IPaymentScheduleRepository scheduleRepository, IPaymentFlowSummaryService summaryService)
        {
            _context = context;
            _proposalRepository = proposalRepository;
            _summaryRepository = summaryRepository;
            _scheduleRepository = scheduleRepository;
            _summaryService = summaryService;
        }

        public async Task<Proposal> CreateProposal(Proposal proposal)
        {

            

            return await _proposalRepository.AddAsync(proposal);
        }

       
    }

}

