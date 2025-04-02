using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PaymentSchedule")]
public class PaymentSchedule
{
      public int Id { get; set; }

    [Required]
    public double Principal { get; set; }

    [Required]
    public double Balance { get; set; }

    [Required]  
    public double Interest { get; set; }

    [Required]
    public int PaymentSummaryId { get; set; }

    public virtual PaymentFlowSummary? PaymentFlowSummary { get; set; }
}