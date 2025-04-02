using System.Linq;
using System.Threading.Tasks;
using Application.InterfaceServices;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Services

{
    public class UserService : IUserService
    {
        private readonly BaseContextGlobalhit _context;
        private readonly IUserRepository _userRepository;


        public UserService(BaseContextGlobalhit context, IUserRepository userRepository)
        {

            _context = context;
            _userRepository = userRepository;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == password);

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> CreateUserAsync(string username, string password, IEnumerable<string> roles)
        {
            if (roles == null || !roles.Any())
            {
                throw new ArgumentException("É necessário fornecer pelo menos uma role.");
            }

            var existingUser = (await _userRepository.GetAllAsync()).FirstOrDefault(x => x.UserName == username);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Usuário já existe.");
            }

            var lstRole = _context.Roles.Where(r => roles.Contains(r.Name)).ToList();

            var user = new User
            {
                UserName = username,
                PasswordHash = password, 
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var createdUser = await _userRepository.AddAsync(user);

                foreach (var role in roles)
                {
                    var matchingRole = lstRole.FirstOrDefault(x => x.Name == role);
                    var roleId = matchingRole != null ? matchingRole.Id : 2;

                    if (matchingRole == null)
                    {
                        Console.WriteLine($"A role '{role}' não foi encontrada. Usando a role padrão de ID 2.");
                    }

                    var userRole = new UserRole
                    {
                        UserId = createdUser.Id,
                        RoleId = roleId,
                    };

                    await _context.UserRoles.AddAsync(userRole);
                    _context.SaveChanges();
                }

                await transaction.CommitAsync();
                return createdUser;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
