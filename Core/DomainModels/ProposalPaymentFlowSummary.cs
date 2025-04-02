using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModels;

public class ProposalPaymentFlowSummary
{
    public int ProposalId { get; set; }
    public int PaymentFlowSummaryId { get; set; }

    public Proposal Proposal { get; set; }
    public PaymentFlowSummary PaymentFlowSummary { get; set; }

}
