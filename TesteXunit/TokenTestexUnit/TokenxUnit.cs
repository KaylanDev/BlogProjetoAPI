using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Infrastruture.Services;
using Blog_Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteXunit.TokenTestexUnit
{
    public class TokenxUnit : IDisposable
    {
        public readonly ITokenService _tokenService;

        public TokenxUnit()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            _tokenService = new TokenService(new Token(configuration));
        }

        public void Dispose()
        {
            
        }
    }
}
