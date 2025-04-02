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
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(BaseContextGlobalhit context) : base(context)
		{
		}
	}
}
