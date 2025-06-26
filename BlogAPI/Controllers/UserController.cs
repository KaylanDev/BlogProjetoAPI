using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new Exception("teste concluido");
            var users = await _userService.GetAsync();
            if (users is null || users.Count() == 0)
            {
                return BadRequest("N ha usuarios!");
            }
            return Ok(users);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int  id)
        {
            if (id <= 0) return BadRequest("Id invalido!");

            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto,string password)
        {    
           
            if (userDto == null || string.IsNullOrEmpty(password))
            {
                return BadRequest("dados invalidos");
            }
            
            var createdUser = await _userService.CreateAsync(userDto,password);
            return CreatedAtAction(nameof(GetById), new { name = createdUser.UserName }, createdUser);
        }
        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name,UserDTO userDto)
        {
           
            if (userDto == null || string.IsNullOrEmpty(userDto.UserName) ||name is null)
            {
                return BadRequest("Dados invalidos");
            }
            var updatedUser = await _userService.UpdateAsync(userDto,name);

            if (updatedUser is null) return BadRequest("senha invalida!");
            
            return Ok(updatedUser);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string username, string password)
        {
            if (username == null || password == null)
            {
                return BadRequest("Dados invalidos");
            }
            var deletedUser = await _userService.DeleteAsync(username,password);
            if (!deletedUser) return BadRequest("Senha invalida");
            return Ok(deletedUser);
        }
    }
}
