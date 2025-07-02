using Blog.API.Pagination;
using Blog.API.PaginationHandler;
using Blog.Application.Interfaces;
using Blog.Application.DTOs.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Application.DTOs.UserModel;
using Blog.Application.DTOs;
using Blog_Domain.Models;
using Blog.Application.DTOs.PostsDTOModel;

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
        
        [HttpGet("Paged_List_Post")]
        public async Task<ActionResult<PagedList<PostDTOComents>>> Get([FromQuery] PaginationParams paginationParams)
        {
            // 'AsQueryable()' é importante se você estiver trabalhando com IQueryable (ex: Entity Framework)
            // para que o Skip/Take sejam traduzidos para SQL
            var produtosQuery = await _userService.GetPostsAndComents();

            var pagedProdutos = PagedList<PostDTOComents>.ToPagedList(
                produtosQuery.Posts,
                paginationParams.PageNumber,
                paginationParams.PageSize
            );

            // Adicionar os metadados de paginação aos headers HTTP é uma boa prática RESTful
            Response.Headers.Append("X-Pagination",
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    pagedProdutos.PageNumber,
                    pagedProdutos.PageSize,
                    pagedProdutos.TotalCount,
                    pagedProdutos.TotalPages,
                    pagedProdutos.HasPrevious,
                    pagedProdutos.HasNext
                }, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }));

            

            return Ok(pagedProdutos); // Retorna a lista de produtos da página
        //}
}

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int  id)
        {
            if (id <= 0) return BadRequest("Id invalido!");

            var user = await _userService.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound("Usuario n encontrado");
            }
            return Ok(user);
        }

        [HttpGet("Posts")]
        public async Task<IActionResult> GetPostsAndComents()
        {
            var user = await _userService.GetPostsAndComents();
            if (user is null)
            {
                return NotFound("Usuario n encontrado");
            }
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
            return CreatedAtAction(nameof(GetById), new { id = createdUser.UserId }, createdUser);
        }
        [HttpPut("{password}")]
        public async Task<IActionResult> Update(string password,UserDTO userDto)
        {
           
            if (userDto == null || string.IsNullOrEmpty(userDto.UserName) ||password is null)
            {
                return BadRequest("Dados invalidos");
            }
            var updatedUser = await _userService.UpdateAsync(userDto,password);

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
            return Ok($"Usuario {username} deletado com sucesso!");
        }
    }
}
