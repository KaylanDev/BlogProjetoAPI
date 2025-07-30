using Blog.Application.DTOs.UserModel;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using FluentResults;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository; 
        private readonly IUserService _usersService; 
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher passwordHasher = new PasswordHasher();

        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IUserService usersService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _usersService = usersService;
        }

        public async Task<Result<string>> Login(string username, string password)
        {
            if (username is null || password is null) return Result.Fail("Username or password cannot be null");

            var user = await _authRepository.GetUserByName(username);
            
            if (user is null) return Result.Fail("User not found");
            var result = CheckPasswordAsync(user, password);

            if (!result)
            {
                return Result.Fail("Invalid password");
            }
           
           var token =  _tokenService.GenerateJwtToken(user);
            var refreshhToken = _tokenService.GenerateRefreshToken();
            if (refreshhToken.IsFailed)
            {
                return Result.Fail("Failed to generate refresh token");
            }
            if (token.IsFailed)
            {
                return Result.Fail("Failed to generate token");
            }

            var exintingToken = await _authRepository.GetByRefreshByUserId(user.UserId);
            // Atualiza o refresh token existente
            exintingToken.Token = refreshhToken.Value;
                    await _authRepository.UpdateAsync(exintingToken);
            
            return token.Value;
        }

        public async Task<Result<string>> Register(UserDTORegister userDto)
        {
            if (userDto is null) return Result.Fail("User nao pode ser null");
            var existingUser = await _usersService.GetByNameAsyncAsync(userDto.Username);

            if (existingUser != null) return Result.Fail("User ja existe");
            if (userDto.Password != userDto.ConfirmPassword) return Result.Fail("senhas divergentes!");
            
            var createdUser = await _usersService.CreateAsync(userDto, userDto.Password);
            if (createdUser is null)
            {
                return Result.Fail("Failed to create user");
            }
            var token = _tokenService.GenerateJwtToken(createdUser);
            var refreshhToken = _tokenService.GenerateRefreshToken();
            
            if (token.IsFailed)return Result.Fail("Failed to generate token");
            if(refreshhToken.IsFailed) return Result.Fail("Failed to generate refresh token");

            // Salva o refresh token no banco de dados
            await _authRepository.CreateUser(createdUser, refreshhToken.Value);

            return token.Value;
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
    }
}
