using Blog.Infrastruture.Context;
using Blog.Infrastruture.Services;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Repository
{
    public class AuthRepository : Repository<RefreshToken>, IAuthRepository
    {
        public AuthRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> CreateUser(User user, string token)
        {
       var userdsjkdsa = await CreateUseradfjkasdjksa(user);
            
            var refreshToken = new RefreshToken
            {
                UserId = userdsjkdsa.UserId,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2),
                IsRevoked = false
            };
           
            await _context.RefreshToken.AddAsync(refreshToken);
           await _context.SaveChangesAsync();
            return userdsjkdsa;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshToken
              .FirstOrDefaultAsync(r => r.Token == token);

            if (refreshToken == null)
                return false;

            if (refreshToken.Expiration < DateTime.UtcNow || refreshToken.IsRevoked)
                return false;

            return true;
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshToken
                .FirstOrDefaultAsync(r => r.Token == token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                _context.RefreshToken.Update(refreshToken);
                await _context.SaveChangesAsync();
            }

        }

       public async Task<User> GetUserByName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
        public async Task<RefreshToken> GetByRefreshByUserId(int userId)
        {
            return await _context.RefreshToken.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        private async Task<User> CreateUseradfjkasdjksa(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
