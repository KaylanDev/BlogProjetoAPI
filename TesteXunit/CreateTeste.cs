using Blog.Infrastruture.Context;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteXunit.RepositoryTestexUnit;

namespace TesteXunit;

public class CreateTeste : IClassFixture<UserManagerFixture>
{
    private readonly UserManager<User> _userManeger;
    private readonly AppDbContext _context; // Para acessar o banco de dados diretamente no assert

    public CreateTeste(UserManagerFixture usf)
    {
        _userManeger = usf.UserManager;
        _context = usf.Context; // Obter o contexto do fixture para acessar o banco de dados
    }

    [Fact]
    public async Task Create_Should_Succeed() // Nomes de métodos de teste devem ser claros
    {
        // Arrange
        var user = new User { UserName = "paulin", Email = "paulin@teste.com" };
        var password = "Teste@123";

        // Act
        var result = await _userManeger.CreateAsync(user, password); // Use await para obter o resultado do Task<IdentityResult>

        // Assert
        result.Should().NotBeNull();
        result.Succeeded.Should().BeTrue(); // Verifique se a criação foi bem-sucedida


    }



    [Fact]
    public async Task Create_Should_falha()
    {
        //arrenge
        var user = new User { UserName = "naylan", Email = "kaylan@teste.com" };
        //act
        var data = await _userManeger.CreateAsync(user, "te");

        //assert
        data.Errors.Should().NotBeNullOrEmpty();
    }
    // Limpeza após cada teste (importante para testes de integração)
    public void Dispose()
    {
        _context.Dispose(); // Libera os recursos do DbContext
    }
}
