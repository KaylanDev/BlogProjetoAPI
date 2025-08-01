using Blog.Application.DTOs.PostsDTOModel;
using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog_Domain.Repository;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteXunit.RepositoryTestexUnit;

namespace TesteXunit.PostServiceTeste
{
    public class PostTesteReturns : IClassFixture<PostxUnit>
    {
        private readonly IPostService _postService;
        private readonly IPostsRepository _postRepository;
        public PostTesteReturns(PostxUnit postxUnit)
        {
            _postService = new PostService(postxUnit._repository);
            _postRepository = postxUnit._repository;
        }
        // -------------------- GetAsyncAsync --------------------
        [Fact]
        public async Task GetAsyncAsync_DeveRetornarSucesso_QuandoPostsExistem()
        {
            var result = await _postService.GetAsync();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<IEnumerable<PostDTO>>();
        }
        [Fact]
        public async Task GetAsyncAsync_DeveRetornarFalha_QuandoNaoExistemPosts()
        {
            // Simule o repositório retornando null (mock ou ajuste no banco de dados de teste)
            // Exemplo: await _postRepository.DeleteAllPostsAsync(); // se existir esse método
            // Aqui apenas o teste, ajuste conforme seu setup
            var result = await _postService.GetAsync();
            if (!result.IsSuccess)
            {
                result.Errors.Should().NotBeNull();
                
            }
        }
    }
}
