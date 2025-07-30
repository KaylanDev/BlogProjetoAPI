using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Domain.Repository
{
   public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByNameAsync(string name);
        public Task<User> GetPostsAndComents();
       
    }
}
