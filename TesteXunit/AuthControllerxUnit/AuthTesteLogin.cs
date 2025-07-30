using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Blog.API.Controllers;
using Blog.Application.DTOs.UserModel;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TesteXunit.AuthControllerxUnit;

public class AuthTesteLogin : IClassFixture<AuthxUnit>
{
    private readonly AuthController _authController;
    public AuthTesteLogin(AuthxUnit authxUnit)
    {
        _authController = new AuthController(authxUnit.AuthService);
    }
    // Aqui você pode adicionar os testes para o AuthService
    // Exemplo:
     [Fact]
     public async Task Login_Should_Return_Valid_Token()
     {
         var userLogin = new UserDTOLogin { Username = "admin", Password = "teste" };
         var result = await _authController.Login(userLogin);
         Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
     }
    [Fact]
    public async Task Login_Should_Return_BadRequest_When_User_Not_Found()
    {
        // Arrange
        var userLogin = new UserDTOLogin { Username = "usuario_inexistente", Password = "senha_errada" };

        // Act
        var result = await _authController.Login(userLogin);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequest = result as BadRequestObjectResult;
        badRequest.Value.Should().Be("User not found");
    }
}
