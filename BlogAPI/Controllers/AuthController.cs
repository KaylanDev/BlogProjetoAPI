using Blog.API.ExceptionHandler;
using Blog.Application.DTOs.UserModel;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Blog.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
 
    private readonly IAuthService _authService; 

    public AuthController( IAuthService authService)
    {
       
        _authService = authService;
    }
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(UserDTOLogin usedto)
    {
        if (usedto is null)
        {
            return BadRequest(Result<string>.Failure("preencha os campos!"));
        }
        var result = await _authService.Login(usedto.Username,usedto.Password);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors.FirstOrDefault()?.Message);
        }
        return Ok(new { Token = result.Value  ,Message = "Usuario logado com sucesso"});
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserDTORegister userDto)
    {
        if (userDto is null)
        {
            return BadRequest(Result<string>.Failure("preencha os campos!"));
        }
        
        var result = await _authService.Register(userDto);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors.FirstOrDefault()?.Message);
        }
        return Ok(new { Token = result.Value, Message = "Usuario criado com sucesso" });
    }
}