using Blog.Infrastruture.Context;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == name.ToLower());
            return user;
        }

        public async Task<User> GetPostsAndComents()
        {
            var user = await _context.Users
                .Include(u => u.Posts!)
                .ThenInclude(p => p.Coments!).Take(3) // Fix: Garantir propriedade de navegação não anulável
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task SaveRefreshTokenAsync(int userId, string token, DateTime expiration)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                Expiration = expiration,
                IsRevoked = false
            };
            await _context.RefreshToken.AddAsync(refreshToken);
        }

        public async Task<int?> ValidateRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshToken
              .FirstOrDefaultAsync(r => r.Token == token);

            if (refreshToken == null)
                return null;

            if (refreshToken.Expiration < DateTime.UtcNow || refreshToken.IsRevoked)
                return null;

            return refreshToken.UserId;
        }
    }
}
