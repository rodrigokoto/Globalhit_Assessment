using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServices
{
	public interface IUserService
	{
		Task<User> AuthenticateAsync(string username, string password);
		Task<User> CreateUserAsync(string username, string password, IEnumerable<string> roles);
	}
}
