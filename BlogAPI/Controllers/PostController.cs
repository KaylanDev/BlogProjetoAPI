using Blog.Application.DTOs.PostsDTOModel;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAsync();
            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found.");
            }
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Id invalido");
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound($"Post with id {id} not found.");
            }
            PostDTO postDto = post;
            return Ok(postDto);
        }

        [HttpGet("Get_tittle")]
        public async Task<IActionResult> GetPostByTittle(string tittle)
        {
            if (string.IsNullOrWhiteSpace(tittle))
            {
                return BadRequest("O titulo n pode estar vazio");
            }
            var posts = await _postService.GetPostByTittle(tittle);
            if (posts == null || !posts.Any())
            {
                return NotFound($"Post com o titulo '{tittle}' nao encontrado.");
            }
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostDTO postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post cannot be null.");
            }
            Post Post = postDto;
            var createdPost = await _postService.CreateAsync(Post);
            if (createdPost == null)
            {
                return BadRequest("Failed to create postDto.");
            }
            //return CreatedAtAction(nameof(Get), new { id = createdPost.PostId }, createdPost);
            return Ok(createdPost);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PostDTO postDto)
        {
            if (postDto == null || postDto.Id != id)
            {
                return BadRequest("Dados invalidos ou Id invalido");
            }
            var updated = await _postService.UpdateAsync(postDto);
            if (!updated)
            {
                return BadRequest("ocorreu um erro ao salvar alteracoes");
            }
            return CreatedAtAction(nameof(GetById), new { id = postDto.Id }, postDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Id invalido");
            var deleted = await _postService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Post with id {id} not found.");
            }
            return Ok($"Post com Id {id} Deletado com sucesso!");
        }
    }
}
