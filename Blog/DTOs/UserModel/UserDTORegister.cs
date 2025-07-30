using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.UserModel
{
    public class UserDTORegister
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserDTORegister()
        {
            
        }
        public  static implicit operator UserDTO( UserDTORegister userDTO )
        {
            return new UserDTO
            {
                UserName = userDTO.Username,
                Email = userDTO.Email,  
            };
        }

    }
}
