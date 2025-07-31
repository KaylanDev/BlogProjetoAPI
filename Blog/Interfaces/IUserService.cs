using Blog.Application.DTOs.UserModel;
using Blog_Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces;

public interface IUserService
{
    public Task<Result<IEnumerable<UserDTO>>> GetAsyncAsync();
    public Task<Result<UserDTO>> GetByNameAsync(string name);
    public Task<Result<UserDTO>> GetByIdAsync(int id);
    public Task<Result<User>> GeneratorPasswordHash(UserDTO userDto,string password);
   public  Task<Result<UserDTO>> UpdateAsync(UserDTO userDto,string password);
   public  
        DeleteAsync(string username,string password);
    public Task<Result<UserDTOPosts>> GetPostsAndComents();
   


}
