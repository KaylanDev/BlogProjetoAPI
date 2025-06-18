using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.Extensions
{
 public static   class UserDTOExtensions
    {
        public static IEnumerable<UserDTO> UserForDTOLIst(this IEnumerable<User> users)
        {
            return users.Select(user => (UserDTO)user);
        }
    }
}
