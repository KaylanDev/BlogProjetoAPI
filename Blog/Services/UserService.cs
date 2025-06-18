using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.DTOs.Extensions;
using Microsoft.AspNetCore.Identity;
using Blog_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDTO = users.UserForDTOLIst();
            
            return userDTO;
        }

        public async Task<UserDTO> GetByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            
            if (user == null)
            {
                throw new ArgumentNullException("Usuario n encontrado!"); // or throw an exception if preferred
            }
            return user;
        }
        public async Task<UserDTO> Update(UserDTO userDto,string name)
        {
            User user = userDto;
            var userveridic = await _userManager.FindByNameAsync(name);
            if ( !(await _userManager.CheckPasswordAsync(userveridic,userDto.Password)))
            {
                return null;
            }
            userveridic.UserName = user.UserName;
            userveridic.Email = user.Email;
            await _userManager.UpdateAsync(userveridic);

            return userDto; 
        }
        public async Task<UserDTO> Create(UserDTO userDto)
        {
            var user = userDto; 
          var result =  await _userManager.CreateAsync(user,userDto.Password);

            if (result.Errors.Count() > 0)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return userDto;
        }

        public async Task<UserDTO> Delete(UserDTO userDto)
        {
            
            var userveridic = await _userManager.FindByNameAsync(userDto.UserName);
            if (!(await _userManager.CheckPasswordAsync(userveridic, userDto.Password)))
            {
                return null;
            }
    
            var result = await _userManager.DeleteAsync(userveridic);
            if (result.Errors.Count() > 0)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return userDto;
        }


    }
}
