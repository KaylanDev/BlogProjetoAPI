using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Blog_Domain.Models;

namespace Blog.Application.DTOs;

public      class UserDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserDTO()
    {
        
    }

    // Implicit operator para converter User para UserDTO
    public static implicit operator UserDTO(User user )
    {
        return new UserDTO
        {
            UserName = user.UserName,
            Email = user.Email,
        };

    }
    //Implicit operator para converter User para UserDTO
    public static implicit operator User(UserDTO user)
    {
        return new User
        {
            UserName = user.UserName,
            Email = user.Email,
        };

    }

   

}
