using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.Extensions
{
  public static  class PostDTOExtensions
    {
        public static IEnumerable<PostDTO> PostsForDTOLIst(this IEnumerable<Post> users)
        {
            return users.Select(user => (PostDTO)user);
        }
    }
}
