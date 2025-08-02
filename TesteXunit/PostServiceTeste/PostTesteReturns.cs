using Blog.Application.DTOs.PostsDTOModel;
using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog_Domain.Repository;
using FluentAssertions;
using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using TesteXunit.RepositoryTestexUnit;
using Moq;

namespace TesteXunit.PostServiceTeste
{
    public class PostTesteReturns : IClassFixture<PostxUnit>
    {
        private readonly IPostService _postService;
        private readonly IPostsRepository _postRepository;
      

        public PostTesteReturns(PostxUnit postxUnit)
        {
            var moc = new Mock<IPostService>();
            
            _postService = new PostService(postxUnit._repository);
            _postRepository = postxUnit._repository;
        }

        // -------------------- GetAsync --------------------

        [Fact]
        public async Task GetAsync_DeveRetornarSucesso_QuandoPostsExistem()
        {
            var result = await _postService.GetAsync();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<IEnumerable<PostDTO>>();
        }

        [Fact]
        public async Task GetAsync_DeveRetornarFalha_QuandoNaoExistemPosts()
        {
            // Simule o repositório retornando null (mock ou ajuste no banco de dados de teste)
            //act
            var mockPostService = new Mock<IPostService>();
            mockPostService.Setup(service => service.GetAsync())
                .ReturnsAsync(Result.Fail<IEnumerable<PostDTO>>("Posts Not found"));
            IPostService postService = mockPostService.Object;
            var result = await postService.GetAsync();
            if (!result.IsSuccess)
            {
                result.Errors.Should().NotBeNull();
                result.Errors.Should().Contain(e => e.Message == "Posts Not found");
            }
        }

        // -------------------- GetByIdAsync --------------------

        [Fact]
        public async Task GetByIdAsync_DeveRetornarSucesso_QuandoPostExiste()
        {
            var result = await _postService.GetByIdAsync(1);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<PostDTO>();
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarFalha_QuandoPostNaoExiste()
        {
            var result = await _postService.GetByIdAsync(-1);
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Message == "Posts Not found");
        }

        // -------------------- GetPostByTittle --------------------

        [Fact]
        public async Task GetPostByTittle_DeveRetornarSucesso_QuandoPostsComTituloExistem()
        {
            var result = await _postService.GetPostByTittle("Post");
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<IEnumerable<PostDTO>>();
        }

        [Fact]
        public async Task GetPostByTittle_DeveRetornarFalha_QuandoPostsComTituloNaoExistem()
        {
            var result = await _postService.GetPostByTittle("tituloInexistente");
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Message == "N foi encontrado Posts com esse titulo");
        }

        // -------------------- CreateAsync --------------------

        [Fact]
        public async Task CreateAsync_DeveRetornarSucesso_QuandoPostValido()
        {
            var postDto = new PostDTO { Title = "Novo Post", Content = "Conteúdo", ImageUrl = "", UserId = 1 };
            var result = await _postService.CreateAsync(postDto);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<PostDTO>();
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarFalha_QuandoPostForNulo()
        {
            var result = await _postService.CreateAsync(null);
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Message == "Post não pode ser nulo!");
        }

        // -------------------- UpdateAsync --------------------

        [Fact]
        public async Task UpdateAsync_DeveRetornarSucesso_QuandoPostValido()
        {
            var postDto = new PostDTO { Id = 1, Title = "Atualizado", Content = "Conteúdo atualizado", ImageUrl = "", UserId = 1 };
            var result = await _postService.UpdateAsync(postDto);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_DeveRetornarFalha_QuandoPostForNulo()
        {
            var result = await _postService.UpdateAsync(null);
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Message == "Post não pode ser nulo!");
        }

        [Fact]
        public async Task UpdateAsync_DeveRetornarFalha_QuandoPostNaoExiste()
        {
            var postDto = new PostDTO { Id = -1, Title = "Inexistente", Content = "Conteúdo", ImageUrl = "", UserId = 1 };
            var result = await _postService.UpdateAsync(postDto);
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Message == "Post not found");
        }

        // -------------------- DeleteAsync --------------------

        [Fact]
        public async Task DeleteAsync_DeveRetornarSucesso_QuandoPostExiste()
        {
            var result = await _postService.DeleteAsync(12);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalha_QuandoPostNaoExiste()
        {
            var result = await _postService.DeleteAsync(-1);
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Message == "Post not found");
        }
    }
}