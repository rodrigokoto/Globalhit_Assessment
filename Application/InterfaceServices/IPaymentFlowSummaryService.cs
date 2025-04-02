using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModels;
using Core.Models;

namespace Application.InterfaceServices
{
    public interface IPaymentFlowSummaryService
    {
        Task<PaymentFlowSummary> ReturnFlowSummaryCalculated(int flowSummaryId);
        Task<PaymentFlowSummary> CreateandCalculateFlowSummary(Proposal proposal);
    }
}
