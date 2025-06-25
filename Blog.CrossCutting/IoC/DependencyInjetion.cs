using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Infrastruture.Context;
using Blog.Infrastruture.Repository;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CrossCutting.IoC;

public static class  DependencyInjetion
{
    public static IServiceCollection Addinfrastruture(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here
          services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BlogDatabase")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<IPostsRepository,PostsRepository>();
        services.AddScoped<IPostService,PostService>();
        services.AddScoped<IComentsRepository,ComentsRepository>();
        services.AddScoped<IComentsService,ComentsService>();

        return services;
    }

}
