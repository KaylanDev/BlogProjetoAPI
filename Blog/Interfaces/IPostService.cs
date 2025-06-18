using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
 public  interface IPostService
    {
        Task<IEnumerable<Post>> GetAsync();
        Task<Post> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post entity);
        Task<bool> UpdateAsync(Post entity);
        Task<bool> DeleteAsync(int id);


    }
}
