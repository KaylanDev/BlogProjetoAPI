using Blog.Infrastruture.Context;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Repository
{
    public class ComentsRepository : Repository<Coment>, IComentsRepository
    {
        public ComentsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
