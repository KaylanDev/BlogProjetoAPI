using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Blog_Domain.Models;

namespace Blog.Application.DTOs;

public   class UserDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
   
    public UserDTO()
    {
        
    }

    // Implicit operator para converter User para UserDTO
    public static implicit operator UserDTO(User user )
    {
        return new UserDTO
        {
            UserId = user.UserId,
            UserName = user.Username,
            Email = user.Email,
        };

    }
    //Implicit operator para converter User para UserDTO
    public static implicit operator User(UserDTO user)
    {
        return new User
        {
            UserId = user.UserId,
            Username = user.UserName,
            Email = user.Email,
        };

    }

   

}
