using E_Commerce.core.Entities.Identity;
using E_Commerce.core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class TokenServices : ITokenService
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public TokenServices(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(ApplicationUser applicationUser)
        {
            var claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Email, applicationUser.Email),
                new Claim(ClaimTypes.Name, applicationUser.DisplayName)
                
            };
            //var roles = await _userManager.GetRolesAsync(applicationUser);
            //claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var cradintials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor() 
            {
                SigningCredentials = cradintials,
                Subject =new ClaimsIdentity(claims),
                Issuer = _config["Token:Issure"],
                Audience = _config["Token:Audiance"],
                Expires = DateTime.Now.AddDays(1)
            };

            var tokenHandeler = new JwtSecurityTokenHandler();
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            return tokenHandeler.WriteToken(token);
        }
    }
}
