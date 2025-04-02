using Application.InterfaceServices;
using Core.DTO;
using Core.DTO.Requests;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlobalHit_Assesment.Controllers
{

    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService; // Serviço para verificar usuários
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }
        /// <summary>
        /// Metod to login into plataform
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDto model)
        {
            // Verificar se o usuário existe e a senha está correta
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return Unauthorized();

            // Criar lista de claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName) // Adiciona o claim do nome
    };

            // Adiciona roles ao token
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            // Verificar se a chave é suficientemente longa
            if (Encoding.UTF8.GetBytes(_key).Length < 32)
            {
                throw new Exception("A chave secreta deve ter pelo menos 32 caracteres.");
            }

            // Gerar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = $"{tokenString}" });
        }

        /// <summary>
        /// Create a new user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = await _userService.CreateUserAsync(request.Username, request.Password, request.Roles);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
