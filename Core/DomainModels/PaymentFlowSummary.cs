using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.BaseModel;

[Table("PaymentFlowSummary")]
public class PaymentFlowSummary : IdentityBase
{
  
    [Required]
    public double MonthlyPayment { get; set; }

    [Required]
    public double TotalInterest { get; set; }

    [Required]
    public double TotalPayment { get; set; }

    public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();

    public ICollection<ProposalPaymentFlowSummary> ProposalPaymentFlowSummaries { get; set; }


}