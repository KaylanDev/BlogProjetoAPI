using Blog.Application.Interfaces;
using Blog.Infrastruture.Services;
using Blog_Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteXunit.TokenTestexUnit
{
    public class TokenTesting : IClassFixture<TokenxUnit>
    {
        private readonly ITokenService _tokenService;
        public TokenTesting(TokenxUnit tokenxUnit)
        {
            _tokenService = tokenxUnit._tokenService;
        }
        [Fact]
        public void GenerateToken_Should_Return_Valid_Token()
        {
            // Arrange
            var userId = new User
            {
                UserId = 1, // Exemplo de ID de usuário
                Username = "testuser",
                Email = "dskdaks@gmail.com"
                
                
            }; // Exemplo de ID de usuário
            // Act
            var token = _tokenService.GenerateJwtToken(userId);
            // Assert
            Assert.NotNull(token);
            Assert.IsType<Result<string>>(token);
            Assert.NotEmpty(token.Value);
        }

        [Fact]
        public void ValidateRefreshToken_Should_Return_True_For_Valid_Token()
        {
            // Arrange
            var userId = new User
            {
                UserId = 1, // Exemplo de ID de usuário
                Username = "testuser",
                Email = "sdasdds@gmail.com"
            };
            // Act 
            var refreshToken = _tokenService.GenerateRefreshToken();

            //assert
            Assert.NotNull(refreshToken);
            Assert.IsType<Result<string>>(refreshToken);
            Assert.NotEmpty(refreshToken.Value);
        }

        public void Dispose()
        {
            // Implementar a limpeza de recursos, se necessário
        }
    }
}
