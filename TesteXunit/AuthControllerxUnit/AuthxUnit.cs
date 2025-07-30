using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Infrastruture.Context;
using Blog.Infrastruture.Repository;
using Blog.Infrastruture.Services;

using Blog_Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace TesteXunit.AuthControllerxUnit
{
    public class AuthxUnit : IDisposable
    {
        public readonly IAuthService AuthService;


        public AuthxUnit()
        {
            // Configuração em memória para JWT
            var inMemorySettings = new Dictionary<string, string> {
                {"JWT:ValidIssuer", "TestIssuer"},
                {"JWT:ValidAudience", "TestAudience"},
                {"JWT:TokenValidityInMinutes", "15"},
                {"JWT:Secretkey", "SuaChaveSuperSecretaDe32Caracteres!!"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Instancia o tokenService real
            var tokenService = new TokenService(new Token(configuration));

            //string de conexao do banco de testes
            string conectionString = "Data Source=SHEISLINDA;Initial Catalog=BlogDB;" +
                "Integrated Security=True;Trust Server Certificate=True";
            var contextoptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(conectionString).Options;


            var Context = new AppDbContext(contextoptions);
            Context.Database.EnsureDeleted(); // Limpa o banco de dados antes de cada teste
            Context.Database.Migrate();       // Aplica todas as migrações e cria as tabelas corretamente
            
            
            
            var authRepository = new AuthRepository(Context);
            var userService = new UserService(new UserRepository(Context));

            AuthService = new AuthService(authRepository, tokenService,userService);


        }

        public void Dispose()
        {
            // Limpeza de recursos se necessário
        }
    }
}
