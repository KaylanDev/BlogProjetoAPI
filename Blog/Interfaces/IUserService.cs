using Blog.Application.DTOs.UserModel;
using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserDTO>> GetAsyncAsync();
    public Task<UserDTO> GetByNameAsyncAsync(string name);
    public Task<UserDTO> GetByIdAsync(int id);
    public Task<User> CreateAsync(UserDTO userDto,string password);
   public  Task<UserDTO> UpdateAsync(UserDTO userDto,string password);
   public  Task<bool> DeleteAsync(string username,string password);
    public Task<UserDTOPosts> GetPostsAndComents();
   


}
