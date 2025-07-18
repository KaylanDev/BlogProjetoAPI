using Blog.Application.DTOs.UserModel;
using Blog_Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
    public interface ITokenService 
    {
        public string GenerateJwtToken(UserDTO user);
        public string GenerateRefreshToken();
    }
}
