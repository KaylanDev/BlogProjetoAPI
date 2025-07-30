using Blog.Application.DTOs.UserModel;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Blog_Domain.Services;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IToken _token;

        public TokenService(IToken token)
        {
            _token = token;
        }

        public Result<string> GenerateJwtToken(UserDTO userdto)
        {
            User user = userdto;

            return _token.GenerateJwtToken(user);
        }

        public Result<string> GenerateRefreshToken()
        {
            return _token.GenerateRefreshToken();
        }
    }
}
