using Blog_Domain.Models;
using Blog_Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Services
{
    public class Token : IToken
    {
        private readonly IConfiguration _configuration; 

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[] {
   
               new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
               new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
               new Claim(JwtRegisteredClaimNames.Email, user.Email)
             };


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT").GetValue<string>("Secretkey")));
            if (key is null)
            {
                throw new Exception("JWT Secret key is not configured.");
            }
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
                audience: _configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.GetSection("JWT").GetValue<int>("TokenValidityInMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        }
    }
}
