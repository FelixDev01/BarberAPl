using BarberAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioDTO usuarioDTO)
        {
            var user = new IdentityUser 
            {
                UserName = usuarioDTO.Email, Email = usuarioDTO.Email, EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, usuarioDTO.Password);
            
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(GeraToken(usuarioDTO));
            }       
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUsuario(UsuarioDTO usuarioDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDTO.Email, usuarioDTO.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(GeraToken(usuarioDTO));
            }
            else
            {
                return BadRequest("Login inválido.");
            }
        }

        private UsuarioToken GeraToken(UsuarioDTO userinfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userinfo.Email),
                new Claim("Barbearia", "UsuarioBarbearia"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["Jwt:ExpireHours"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais
            );


            return new UsuarioToken()
            {
                Authenticated = true,
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Token JWT gerado com sucesso."
            };
        }
    }
}
