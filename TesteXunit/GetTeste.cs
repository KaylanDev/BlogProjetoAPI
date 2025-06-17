using Blog_Domain.Models;
using Blog_Domain.Repository;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteXunit.RepositoryTestexUnit;

namespace TesteXunit;

public class GetTeste : IClassFixture<RepositoryxUnit>    
{
    private readonly IRepository<Post> repository;

    public GetTeste(RepositoryxUnit repositoryxUnit)
    {
        repository = repositoryxUnit._repository;
    }

    [Fact]
    public void GetAllPosts_ShouldReturnAllPosts()
    {
        // Arrange
        var expectedCount = 3; // Adjust based on your test data
        // Act
        var posts = repository.Get();
        // Assert
       
        posts.Should().NotBeNull();
    }

}
