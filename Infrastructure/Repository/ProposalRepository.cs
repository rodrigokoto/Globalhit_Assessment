using Core.DomainModels;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
	public class ProposalRepository : BaseRepository<Proposal>, IProposalRepository
	{
		public ProposalRepository(BaseContextGlobalhit context) : base(context)
		{
		}

    }
}
