using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using TesteXunit.RepositoryTestexUnit;
using Blog.Application.Interfaces;
using Blog_Domain.Repository;
using Blog.Application.Services;
using FluentResults;
using Blog.Application.DTOs.ComentDTOModel;

namespace TesteXunit.ComentServiceTeste
{
    public class ComentTesteReturns : IClassFixture<ComentsxUnit>
    {
        private readonly IComentsService _comentsService;
        private readonly IComentsRepository _comentsRepository;

        public ComentTesteReturns(ComentsxUnit comentsxUnit)
        {
            _comentsService = new ComentsService(comentsxUnit._repository);
            _comentsRepository = comentsxUnit._repository;
        }

        // -------------------- GetAsync --------------------

        [Fact]
        public async Task GetAsync_DeveRetornarSucesso_QuandoComentsExistem()
        {
            var result = await _comentsService.GetAsync();
            result.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<IEnumerable<ComentsDTO>>();
        }

        [Fact]
        public async Task GetAsync_DeveRetornarNull_QuandoNaoExistemComents()
        {
            var mockComentService = new Mock<IComentsService>();
            mockComentService.Setup(service => service.GetAsync())
                .ReturnsAsync((Result<IEnumerable<ComentsDTO>>)null);
            var result = await mockComentService.Object.GetAsync();
            result.Should().BeNull();
        }

        // -------------------- GetByIdAsync --------------------

        [Fact]
        public async Task GetByIdAsync_DeveRetornarSucesso_QuandoComentExiste()
        {
            var result = await _comentsService.GetByIdAsync(1);
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<ComentsDTO>();
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNull_QuandoComentNaoExiste()
        {
            var result = await _comentsService.GetByIdAsync(-1);
            result.IsFailed.Should().BeTrue();
        }

        // -------------------- CreateAsync --------------------

        [Fact]
        public async Task CreateAsync_DeveRetornarSucesso_QuandoComentValido()
        {
            var comentDto = new ComentsDTO { Content = "Comentário válido", PostId = 1, UserId = 1 };
            var result = await _comentsService.CreateAsync(comentDto);
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<ComentsDTO>();
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarNull_QuandoComentForNulo()
        {
            var result = await _comentsService.CreateAsync(null);
            result.Errors.Should().Contain(c => c.Message == "Coments cannot be null! ");
        }

        // -------------------- UpdateAsync --------------------

        [Fact]
        public async Task UpdateAsync_DeveRetornarSucesso_QuandoComentValido()
        {
            var comentDto = new ComentsDTO { ComentId = 1, Content = "Atualizado", PostId = 1, UserId = 1 };
            var result = await _comentsService.UpdateAsync(comentDto);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_DeveRetornarFalse_QuandoComentForNulo()
        {
            var result = await _comentsService.UpdateAsync(null);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_DeveRetornarFalse_QuandoComentNaoExiste()
        {
            var comentDto = new ComentsDTO { ComentId = -1, Content = "Inexistente", PostId = 1, UserId = 1 };
            var result = await _comentsService.UpdateAsync(comentDto);
            result.IsSuccess.Should().BeFalse();
        }

        // -------------------- DeleteAsync --------------------

        [Fact]
        public async Task DeleteAsync_DeveRetornarSucesso_QuandoComentExiste()
        {
            var result = await _comentsService.DeleteAsync(1,1);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalse_QuandoComentNaoExiste()
        {
            var result = await _comentsService.DeleteAsync(-1,1);
            result.IsSuccess.Should().BeFalse();
        }
    }
}
