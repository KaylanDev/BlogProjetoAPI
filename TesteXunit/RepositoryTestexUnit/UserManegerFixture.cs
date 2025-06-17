using Blog.Infrastruture.Context;
using Blog_Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TesteXunit.RepositoryTestexUnit;

public class UserManagerFixture : IDisposable
{
    public UserManager<User> UserManager { get; private set; }
    public AppDbContext Context { get; private set; } // Pode ser útil expor o contexto também

    public UserManagerFixture()
    {
        var connectionString = "Data Source=SHEISLINDA;Initial Catalog=BlogDB;Integrated Security=True;Trust Server Certificate=True";
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        Context = new AppDbContext(options);
        Context.Database.EnsureDeleted(); // Limpa no início do fixture
        Context.Database.EnsureCreated();

        var userStore = new UserStore<User, IdentityRole<int>, AppDbContext, int>(Context);

        var passwordHasher = new PasswordHasher<User>();
        var userValidators = new List<IUserValidator<User>> { new UserValidator<User>() };
        var passwordValidators = new List<IPasswordValidator<User>> { new PasswordValidator<User>() };
        var mockLookupNormalizer = new Mock<ILookupNormalizer>();
        mockLookupNormalizer.Setup(x => x.NormalizeName(It.IsAny<string>())).Returns((string name) => name.ToUpperInvariant());
        mockLookupNormalizer.Setup(x => x.NormalizeEmail(It.IsAny<string>())).Returns((string email) => email.ToUpperInvariant());
        var mockErrorDescriber = new IdentityErrorDescriber();
        var mockServiceProvider = new Mock<IServiceProvider>();
        mockServiceProvider.Setup(_ => _.GetService(It.IsAny<Type>())).Returns((Type serviceType) => null);
        var mockLogger = new Mock<ILogger<UserManager<User>>>().Object;
        var mockOptionsAccessor = Options.Create(new IdentityOptions());



        UserManager = new UserManager<User>(
            userStore,mockOptionsAccessor, passwordHasher, userValidators, passwordValidators,
            mockLookupNormalizer.Object, mockErrorDescriber, mockServiceProvider.Object, mockLogger
        );
    }

    public void Dispose()
    {
        Context.Dispose(); // Libera recursos no final da execução de todos os testes da classe
    }
}
