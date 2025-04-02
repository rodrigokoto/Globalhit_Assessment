using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using Application.InterfaceServices;
using Core.DomainModels;
using Globalhit_Assesment.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Globalhit_Assesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IProposalService _proposalService;
        private readonly IPaymentFlowSummaryService _paymentFlowSummaryService;

        public LoanController(IProposalService proposalService, IPaymentFlowSummaryService paymentFlowSummaryService)
        {
            _proposalService = proposalService;
            _paymentFlowSummaryService = paymentFlowSummaryService;
        }

        [Authorize]
        [HttpPost("simulate")]
        public async Task<IActionResult> Simulate(CreatePropostaRequest propostaRequest)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(propostaRequest);

            if (!Validator.TryValidateObject(propostaRequest, validationContext, validationResults, true))
            {
                var error = validationResults.Select(x => x.ErrorMessage).ToList();
                return BadRequest(error);
            }
            else
            {
                var proposal = new Proposal()
                {
                    AnnualInterestRate = propostaRequest.AnnualInterestRate,
                    LoanAmount = propostaRequest.LoanAmount,
                    NumberOfMonths = propostaRequest.NumberOfMonths
                };

                var result = await _proposalService.CreateProposal(proposal);

                var summary = await _paymentFlowSummaryService.CreateandCalculateFlowSummary(result);
                return Ok(summary);
            }

            
        }
    }
}
