using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Domain.Repository
{
    public interface IAuthRepository : IRepository<RefreshToken>
    {
        public Task<User> CreateUser(User user, string token);

        public Task<bool> ValidateRefreshTokenAsync(string token);
        public Task<User> GetUserByName(string username);
        public  Task<RefreshToken> GetByRefreshByUserId(int userId);
    }
}
