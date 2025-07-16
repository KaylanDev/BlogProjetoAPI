using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Domain.Services
{
   public  interface ITokenService
    {
        public string GenerateJwtToken(User user);
        public string GenerateRefreshToken();
    }
}
