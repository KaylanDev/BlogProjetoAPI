using Blog.API.Controllers;
using Blog.Application.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteXunit.RepositoryTestexUnit;

namespace TesteXunit.ComentTeste
{
   public class GetComentxUnit : IClassFixture<ComentsxUnit>
    {
        private readonly ComentsController _controller;

        public GetComentxUnit(ComentsxUnit controller)
        {
            _controller = new ComentsController(new ComentsService(controller._repository));
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenComentsExist()
        {
            // Act
            var result = await _controller.Get();
            // Assert
            //Assert.IsType<OkObjectResult>(result);
            //var okResult = result as OkObjectResult;
            //Assert.NotNull(okResult.Value);
            //Assert.IsAssignableFrom<IEnumerable<Blog.Application.DTOs.ComentsDTO>>(okResult.Value);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_NotFound()
        {
           
            // Act
            var result = await _controller.Get();
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
           
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenComentExists()
        {
            // Arrange
            int id = 1; // Assuming this ID exists in the test data
            // Act
            var result = await _controller.GetById(id);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenComentDoesNotExist()
        {
            // Arrange
            int id = 999; // Assuming this ID does not exist in the test data
            // Act
            var result = await _controller.GetById(id);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be($"Comentario com  id {id} nao encontrado.");
        }
        [Fact]
        public async Task GetById_Id_Invalid()
        {
            // Arrange
            int id = -1; // Invalid ID
            // Act
            var result = await _controller.GetById(id);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Id invalido");
        }
    }
}
