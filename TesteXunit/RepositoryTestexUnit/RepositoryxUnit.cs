using Blog.Infrastruture.Context;
using Blog.Infrastruture.Repository;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteXunit.RepositoryTestexUnit;

public class RepositoryxUnit
{
    public readonly IRepository<Post> _repository;
    public static DbContextOptions<AppDbContext> contextoptions;
    

    

    private const string conectionString = "Data Source=SHEISLINDA;Initial Catalog=BlogDB;Integrated Security=True;Trust Server Certificate=True";

    static RepositoryxUnit()
    {
        contextoptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(conectionString).Options;
    }

    public RepositoryxUnit()
    {
        var context = new AppDbContext(contextoptions);
        _repository = new Repository<Post>(context);
    }


}
