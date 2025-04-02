using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.Requests
{
	public class CreateUserRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}
