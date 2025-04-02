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
	public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
	{
		public PessoaRepository(BaseContextGlobalhit context) : base(context)
		{
		}
	}
}
