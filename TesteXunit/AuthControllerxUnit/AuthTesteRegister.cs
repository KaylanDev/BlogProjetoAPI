using Blog.API.Controllers;
using Blog.Application.DTOs.UserModel;
using Blog_Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteXunit.AuthControllerxUnit;

public class AuthTesteRegister : IClassFixture<AuthxUnit>
{
    private readonly AuthController _authController;
    public AuthTesteRegister(AuthxUnit authxUnit)
    {
        _authController = new AuthController(authxUnit.AuthService);
    }
    [Fact]
    public async Task Register_Should_Return_CreatedResult_When_Success()
    {
        // Arrange
        var userRegister = new UserDTORegister
        {
            Username = "leo",
            Email = "leozin@teste.com",
            Password = "senha",
            ConfirmPassword = "senha"
        };
        // Act
          var result = await _authController.Register(userRegister);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        
    }
}