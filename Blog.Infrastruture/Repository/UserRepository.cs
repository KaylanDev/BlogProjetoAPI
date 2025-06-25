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
    }
}
