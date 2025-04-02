using Core.Models;
using Infrastructure.Repository;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
       
    }
}
