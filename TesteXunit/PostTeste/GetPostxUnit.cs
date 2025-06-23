using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteXunit.RepositoryTestexUnit;

namespace TesteXunit.PostTeste;

public class GetPostxUnit : IClassFixture<PostxUnit>
{
    private readonly IPostService _postService;
    public GetPostxUnit(PostxUnit postxUnit)
    {
        _postService = new PostService(postxUnit._repository);
    }
    [Fact]
    public async Task GetAllPosts_Should_Return_All_Posts()
    {
        // Act

        var result = await _postService.GetAsync();
      
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task Create_Teste()
    {
        // Arrange
      var Post = new Post
      {
          Title = "Teste de criação",
          Content = "Conteúdo do post de teste",
          UserId = 1
      };
        // Act
       var result = await _postService.CreateAsync(Post);
        // Assert
        result.Should().NotBeNull();
        
    }
}
