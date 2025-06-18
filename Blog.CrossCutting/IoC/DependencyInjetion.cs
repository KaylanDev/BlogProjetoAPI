using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Infrastruture.Context;
using Blog.Infrastruture.Repository;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using Microsoft.AspNetCore.Identity;
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
        services.AddIdentity<User, IdentityRole<int>>(opc =>
        {
            //senha
            opc.Password.RequireDigit = true; //requer 1 digito numerico
            opc.Password.RequiredLength = 3; // define o tamanho minimo

            //usuario name
            opc.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // define os caracteres permitidos no nome de usuario


        }).AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserService,UserService>();

        return services;
    }

}
