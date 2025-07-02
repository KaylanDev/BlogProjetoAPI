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
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        public PostsRepository(AppDbContext context) : base(context)
        {



        }

        public async Task<IEnumerable<Post>> GetPostByTittleAsync(string tittle)
        {
            var posts = await _context.Posts
                .Where(p => p.Title.Contains(tittle)).ToListAsync();

            return posts;
        }
    }
}
