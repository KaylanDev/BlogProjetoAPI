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

        public override async Task<bool> DeleteAsync(int id)
        {
            _context.Users.Remove(await _context.Users.FindAsync(id));
            _context.Coments.RemoveRange(_context.Coments.Where(c => c.UserId == id));
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
