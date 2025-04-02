using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.BaseModel;

namespace Core.Models
{
    public class Role : IdentityBase
	{
		
		[Required]
		public string Name { get; set; }
		public ICollection<UserRole> UserRoles { get; set; }
	}
}
