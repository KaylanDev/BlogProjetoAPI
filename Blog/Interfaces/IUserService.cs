using Blog.Application.DTOs;
using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserDTO>> GetAsync();
    public Task<UserDTO> GetByIdAsync(int id);
    public Task<UserDTO> CreateAsync(User user);
   public  Task<UserDTO> UpdateAsync(UserDTO userDto,string name);
   public  Task<bool> DeleteAsync(string username,string password);
   
}
