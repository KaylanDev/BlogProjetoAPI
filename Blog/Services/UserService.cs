using Blog.Application.Interfaces;
using Blog_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.DTOs.Extensions;
using Blog_Domain.Models;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using Blog.Application.DTOs.UserModel;

namespace Blog.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userManager;
    private readonly PasswordHasher passwordHasher = new PasswordHasher();
    public UserService(IUserRepository userManager)
    {
        _userManager = userManager;
    }


    public async Task<IEnumerable<UserDTO>> GetAsyncAsync()
    {
        var users = await _userManager.GetAsync();
        var userDTO = users.UserForDTOLIst();
        
        return userDTO;
    }
    public async Task<UserDTO> GetByNameAsyncAsync(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return null; // or throw an exception if preferred
        }
        var user = await _userManager.GetByNameAsync(name);
        
        if (user == null)
        {
            return null; // or throw an exception if preferred
        }
        return user;
    }
    public async Task<UserDTO> GetByIdAsync(int id)
    {
        var user = await _userManager.GetByIdAsync(id);
        
        if (user == null)
        {
            return null; // or throw an exception if preferred
        }
        return user;
    }
    public async Task<UserDTO> UpdateAsync(UserDTO userDto,string password)
    {
        User user = await _userManager.GetByIdAsync(userDto.UserId);
        
        if (!CheckPasswordAsync(user,password))
        {
            return null;
        }
        user.Username = userDto.UserName;
        user.Email = userDto.Email;
        await _userManager.UpdateAsync(user);
   

        return user; 
    }
    public async Task<User> CreateAsync(UserDTO UserDTO,string password)
    {
        User user = UserDTO;
       user.PasswordHash = HashPassword(password);
        // Verifica se o usuário já existe
        var existingUser = await _userManager. GetByNameAsync(user.Username);
        if (existingUser != null)
        {
            throw new Exception("Usuário já existe.");
        }
        // Cria o usuário 
    


        return user;
    }

    public async Task<bool> DeleteAsync(string name,string password)
    {

        var user = await _userManager.GetByNameAsync(name);
        if (!(CheckPasswordAsync(user, password)))
        {
            return false;
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result)
        {
            throw new Exception("ocorreu um erro ao deletar usuario!");
        }

        return true;
    }

    private bool CheckPasswordAsync(User userveridic, string password)
    {
        if (userveridic is null || password is null)
        {
            return false;
        }
        var result = passwordHasher.VerifyHashedPassword(userveridic.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
    private string HashPassword(string password)
    {
        return passwordHasher.HashPassword(password);
    }

    public async Task<UserDTOPosts> GetPostsAndComents()
    {
       var user = await _userManager.GetPostsAndComents();
       
        if (user == null)
        {
            return null; // or throw an exception if preferred
        }
        UserDTOPosts userdto = user;
        return userdto;
    }
}
