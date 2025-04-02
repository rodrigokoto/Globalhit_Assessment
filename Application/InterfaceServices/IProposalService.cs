using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModels;

namespace Application.InterfaceServices
{
    public interface IProposalService 
    {
        Task<Proposal> CreateProposal(Proposal proposal);    
    }
}
