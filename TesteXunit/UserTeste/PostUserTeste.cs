using Blog.API.Controllers;
using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog_Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteXunit.RepositoryTestexUnit;

namespace TesteXunit.UserTeste
{
    public class PostUserTeste : IClassFixture<UserxUnit>
    {
        private readonly UserController _controller;

        public PostUserTeste(UserxUnit userxUnit)
        {
            _controller = new UserController(new UserService(userxUnit._repository));
        }

        [Fact]
     public async Task Get_Ok_Result()
        {
            //act
            var result = await _controller.Get();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            
        }

        [Fact]
        public async Task GetById_Ok_Result()
        {
            // Arrange
            int id = 1; // Certifique-se de que este ID existe no banco de dados de teste
            // Act
            var result = await _controller.GetById(id);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
