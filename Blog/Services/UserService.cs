using Blog.Application.DTOs;
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

namespace Blog.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userManager;
    private readonly PasswordHasher passwordHasher = new PasswordHasher();
    public UserService(IUserRepository userManager)
    {
        _userManager = userManager;
    }


    public async Task<IEnumerable<UserDTO>> GetAsync()
    {
        var users = await _userManager.GetAsync();
        var userDTO = users.UserForDTOLIst();
        
        return userDTO;
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
        user.Username = user.Username;
        user.Email = user.Email;
        await _userManager.UpdateAsync(user);
   

        return user; 
    }
    public async Task<UserDTO> CreateAsync(User user)
    {
       user.PasswordHash = HashPassword(user.Password);
        // Verifica se o usuário já existe
        var existingUser = await _userManager.GetByNameAsync(user.Username);
        if (existingUser != null)
        {
            throw new Exception("Usuário já existe.");
        }
        // Cria o usuário 
        var result =  await _userManager.CreateAsync(user);

        return user;
    }

    public async Task<bool> DeleteAsync(string name,string password)
    {
        
        var userveridic = await _userManager.GetByNameAsync(name);
        if (!(CheckPasswordAsync(userveridic, password)))
        {
            return false;
        }

        var result = await _userManager.DeleteAsync(userveridic.UserId);
        if (!result)
        {
            throw new Exception("ocorreu um erro ao deletar usuario!");
        }

        return true;
    }

    private bool CheckPasswordAsync(User userveridic, string password)
    {
        var result = passwordHasher.VerifyHashedPassword(userveridic.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
    private string HashPassword(string password)
    {
        return passwordHasher.HashPassword(password);
    }
}
