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
using Microsoft.AspNetCore.Authorization;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var users = await _userService.GetAsyncAsync();
            if (users.Value is null || users.Value.Count() == 0)
            {
                return BadRequest("N ha usuarios!");
            }
            return Ok(users.Value);
        }
        
        [HttpGet("Paged_List_Post")]
        public async Task<ActionResult<PagedList<PostDTOComents>>> Get([FromQuery] PaginationParams paginationParams)
        {
            // 'AsQueryable()' é importante se você estiver trabalhando com IQueryable (ex: Entity Framework)
            // para que o Skip/Take sejam traduzidos para SQL
            var produtosQuery = await _userService.GetPostsAndComents();

            var pagedProdutos = PagedList<PostDTOComents>.ToPagedList(
                produtosQuery.Value.Posts,
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
            return Ok(user.Value);
        }

        [HttpGet("PostsAndComents")]
        public async Task<IActionResult> GetPostsAndComents()
        {
            var user = await _userService.GetPostsAndComents();
            if (user is null)
            {
                return NotFound("Usuario n encontrado");
            }
            return Ok(user.Value);
        }


   
        [HttpPut("{password}")]
        public async Task<IActionResult> Update(string password,UserDTO userDto)
        {
           
            if (userDto == null || string.IsNullOrEmpty(userDto.UserName) ||password is null)
            {
                return BadRequest("Dados invalidos");
            }
            var updatedUser = await _userService.UpdateAsync(userDto,password);

            if (updatedUser.IsFailed) return BadRequest(updatedUser);
            
            return Ok(updatedUser.Value);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string username, string password)
        {
            if (username == null || password == null)
            {
                return BadRequest("Dados invalidos");
            }
            var deletedUser = await _userService.DeleteAsync(username,password);
            if (deletedUser.IsFailed) return BadRequest("Senha invalida");
            return Ok($"Usuario {username} deletado com sucesso!");
        }
    }
}
