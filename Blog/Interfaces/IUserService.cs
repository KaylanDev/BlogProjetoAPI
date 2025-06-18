using Blog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserDTO>> GetAsync();
    public Task<UserDTO> GetByNameAsync(string name);
    public Task<UserDTO> Create(UserDTO userDto);
   public  Task<UserDTO> Update(UserDTO userDto,string name);
   public  Task<UserDTO> Delete(UserDTO userDto);
   
}
