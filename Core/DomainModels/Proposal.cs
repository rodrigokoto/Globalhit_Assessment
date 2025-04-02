using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.BaseModel;

namespace Core.DomainModels
{
    public class Proposal : IdentityBase
    {
        public double LoanAmount { get; set; }

        public double AnnualInterestRate { get; set; } 

        public int NumberOfMonths { get; set; }

        public ICollection<ProposalPaymentFlowSummary> ProposalPaymentFlowSummaries { get; set; }

    }
}
