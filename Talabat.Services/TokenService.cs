using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> GenerateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            // privte claims
            List<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName , user.DisplayName),
                new Claim(ClaimTypes.Email , user.Email)
            };

            var UserRoles = await userManager.GetRolesAsync(user);

            foreach(var Role in UserRoles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            //Register Claims
            var Token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationDay"])),
                signingCredentials : new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
            
        }
    }
}
