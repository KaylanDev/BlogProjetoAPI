using Blog.Application.DTOs.Extensions;
using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.PostsDTOModel
{
   public class PostDTOComents : PostDTO
    {
        IEnumerable<ComentsDTO> Coments { get; set; }

        public PostDTOComents()
        {
            
        }

        public static implicit operator PostDTOComents(Post post)
        {
            return new PostDTOComents
            {
                Id = post.PostId,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                Coments = post.Coments.ComentsForDTOLIst() // Assuming ComentsDTO has an implicit operator from Coment
            };
        }

        public static implicit operator Post(PostDTOComents post)
        {
            return new Post
            {
                PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
               
            };
        }
    }
}
