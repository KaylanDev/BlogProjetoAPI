using Blog.Application.DTOs.UserModel;
using Blog_Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<Result<string>> Login(string username, string password);
        public Task<Result<string>> Register(UserDTORegister user);
    }
}
