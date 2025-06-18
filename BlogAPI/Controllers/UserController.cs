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
            var users = await _userService.GetAsync();
            if (users is null || users.Count() == 0)
            {
                return BadRequest("N ha usuarios!");
            }
            return Ok(users);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (name is null) return BadRequest("O campo Nome é necessario!");

            var user = await _userService.GetByNameAsync(name);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto)
        {    
           
            if (userDto == null || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("dados invalidos");
            }
        
            var createdUser = await _userService.Create(userDto);
            return CreatedAtAction(nameof(GetByName), new { name = createdUser.UserName }, createdUser);
        }
        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name,UserDTO userDto)
        {
           
            if (userDto == null || string.IsNullOrEmpty(userDto.UserName) ||name is null)
            {
                return BadRequest("Dados invalidos");
            }
            var updatedUser = await _userService.Update(userDto,name);

            if (updatedUser is null) return BadRequest("senha invalida!");
            
            return Ok(updatedUser);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserDTO userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.UserName))
            {
                return BadRequest("Dados invalidos.");
            }
            var deletedUser = await _userService.Delete(userDto);
            if (deletedUser is null) return BadRequest("Senha invalida");
            return Ok(deletedUser);
        }
    }
}
