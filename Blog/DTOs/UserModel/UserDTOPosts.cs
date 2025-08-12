using Blog.Application.DTOs.PostsDTOModel;
using Blog_Domain.Models;
using Blog.Application.DTOs.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.DTOs.ComentDTOModel;

namespace Blog.Application.DTOs.UserModel
{
   public class UserDTOPosts : UserDTO
    {
        public IEnumerable<PostDTOComents> Posts { get; set; }
        public IEnumerable<ComentsDTO> Coments  { get; set; }

        public static implicit operator UserDTOPosts(User user)
        {
            return new UserDTOPosts
            {
                UserId = user.UserId,
                UserName = user.Username,
                Email = user.Email,
                Posts = user.Posts?.PostsComentsForDTOLIst(), // Assuming PostDTO has an implicit operator from Post
                Coments = user.Coments?.ComentsForDTOLIst() // Assuming ComentsDTO has an implicit operator from Coment
            };
        

        }
    }
}
