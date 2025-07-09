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
    public string ImagemUrl { get; set; }
    public int UserId { get;  set; }
    

 
    public PostDTO()
    {

    }

    public static implicit operator PostDTO(Post post)
    {
        PostDTO postDto = new PostDTO
        {
            Id = post.PostId,
            Title = post.Title,
            Content = post.Content,
            UserId = post.UserId,
            
        };
        if (post.ImageUrl != null)
        {
            postDto.ImagemUrl = post.ImageUrl;
        }
        return postDto;
    }

    public static implicit operator Post(PostDTO postDto)
    {

       Post post = new Post
        {
            PostId = postDto.Id,
            Title = postDto.Title,
            Content = postDto.Content,
            UserId = postDto.UserId
        };
        if (postDto.ImagemUrl != null )
        {
            post.ImageUrl = postDto.ImagemUrl;
        }
        return post;
    }



}
