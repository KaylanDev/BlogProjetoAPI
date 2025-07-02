using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.PostsDTOModel;

public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get;  set; }

 
    public PostDTO()
    {

    }

    public static implicit operator PostDTO(Post post)
    {
        return new PostDTO
        {
            Id = post.PostId,
            Title = post.Title,
            Content = post.Content,
            UserId = post.UserId
        };
    }

    public static implicit operator Post(PostDTO postDto)
    {

        return new Post
        {
            PostId = postDto.Id,
            Title = postDto.Title,
            Content = postDto.Content,
            UserId = postDto.UserId
        };
    }



}
