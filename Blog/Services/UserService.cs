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
using FluentResults;
using System.Reflection.Metadata.Ecma335;

namespace Blog.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userManager;
    private readonly PasswordHasher passwordHasher = new PasswordHasher();
    public UserService(IUserRepository userManager)
    {
        _userManager = userManager;
    }


    public async Task<Result<IEnumerable<UserDTO>>> GetAsyncAsync()
    {
        var users = await _userManager.GetAsync();
        // Verifica se a lista de usuários é nula ou vazia
        if (users is null)   return Result.Fail("users no  found.");
        
        var userDTO = users.UserForDTOLIst();
        
        return Result.Ok(userDTO);
    }
    public async Task<Result<UserDTO>> GetByNameAsync(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return Result.Fail("Name nao pode ser nulo!"); 
        }
        var user = await _userManager.GetByNameAsync(name);
        
        if (user == null)
        {
            return Result.Fail("User not found"); 
        }

        UserDTO userDto = user;
        return Result.Ok(userDto);
    }
    public async Task<Result<UserDTO>> GetByIdAsync(int id)
    {
        var user = await _userManager.GetByIdAsync(id);
        
        if (user == null)
        {
            return Result.Fail("User not found"); // or throw an exception if preferred
        }
        UserDTO userDto = user;
        return Result.Ok(userDto);
    }
    public async Task<Result<UserDTO>> UpdateAsync(UserDTO userDto,string password)
    {
        User user = await _userManager.GetByIdAsync(userDto.UserId);
        
        if (!CheckPasswordAsync(user,password))
        {
            return Result.Fail("Senha invalida!");
        }
        user.Username = userDto.UserName;
        user.Email = userDto.Email;
        await _userManager.UpdateAsync(user);
   
        UserDTO userDTO = user;
        return Result.Ok(userDto); 
    }
    public async  Task<Result<User>> GeneratorPasswordHash(UserDTO UserDTO,string password)
    {
        User user = UserDTO;
       user.PasswordHash = HashPassword(password);
        return Result.Ok(user);
    }

    public async Task<Result> DeleteAsync(string name,string password)
    {

        var user = await _userManager.GetByNameAsync(name);
        if (!(CheckPasswordAsync(user, password)))
        {
            return Result.Fail("Senha invalida!");
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result)
        {
            return Result.Fail("ocorreu um erro ao deletar usuario!");
        }

        return Result.Ok();
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

    public async Task<Result<UserDTOPosts>> GetPostsAndComents()
    {
       var user = await _userManager.GetPostsAndComents();
       
        if (user == null)
        {
            return Result.Fail("Nao existem Posts deste usuario"); // or throw an exception if preferred
        }
        UserDTOPosts userdto = user;
        return Result.Ok(userdto);
    }
}
