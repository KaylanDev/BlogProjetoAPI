using Blog.API.ExceptionHandler;
using Blog.Application.DTOs.PostsDTOModel;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using FluentResults;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private IWebHostEnvironment _hostingEnvironment;


        public PostController(IPostService postService, IWebHostEnvironment hostingEnvironment)
        {
            _postService = postService;
            _hostingEnvironment = hostingEnvironment;
        }

        private async Task<Result<string>> UploudImg(IFormFile img)
        {
            if (img == null)
            {
                return Result.Fail("Imagem não pode ser nula.");
            }

            string imageUrl = string.Empty;

            if (string.IsNullOrEmpty(imageUrl) && img.Length > 0)
            {
                // Validação do tipo de arquivo (opcional, mas recomendado)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(img.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Result.Fail("Tipo de arquivo não permitido. Apenas JPG, JPEG, PNG e GIF são aceitos.");
                }

                // Garante que a pasta de uploads exista
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Gera um nome de arquivo único para evitar colisões
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                // Constrói a URL para a imagem
                imageUrl = $"{Request.Scheme}://{Request.Host}/Imagens/{uniqueFileName}";

            }

            return Result.Ok(imageUrl);
        }

        private Result<string> UploadDelete(Post post)
        {
            if (post == null || string.IsNullOrEmpty(post.ImageUrl))
            {
                return Result.Fail("Post ou URL da imagem nao existe");
            }
            string imgUrl = post.ImageUrl;

            // Extrai o nome do arquivo da URL
            string fileName = Path.GetFileName(new Uri(imgUrl).AbsolutePath); // resultado: "becff3ab-4e0d-4ce7-8cba-d093c231e302.png"


            var img = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", fileName);



            if (System.IO.File.Exists(img))
            {
                System.IO.File.Delete(img);
                return Result.Ok("Imagem deletada com sucesso.");
            }
            return Result.Fail("Imagem nao encontrada para deletar.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _postService.GetAsync();
            var posts =  result.Value;
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
            PostDTO postDto = post.Value;
            return Ok(postDto);
        }

        [HttpGet("Get_tittle")]
        public async Task<IActionResult> GetPostByTittle(string tittle)
        {
            if (string.IsNullOrWhiteSpace(tittle))
            {
                return BadRequest("O titulo n pode estar vazio");
            }
            var result = await _postService.GetPostByTittle(tittle);
            var posts = result.Value;
            if (posts == null || !posts.Any())
            {
                return NotFound($"Post com o titulo '{tittle}' nao encontrado.");
            }
            return Ok(posts);
        }

        [HttpPost("UploadCreate")]
        public async Task<IActionResult> UploadCreate(IFormFile img)
        {
            if (img == null)
            {
                return BadRequest(Result.Fail("a imagem n pode ser nula!"));
            }
            var result = await UploudImg(img);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete]
        [Route("UploadDelete/{id:int}")]
        public async Task<IActionResult> UploadDelete(int id)
        {
            if (id <= 0) return BadRequest("Id invalido");
            var result = await _postService.GetByIdAsync(id);
            var post = result.Value;
            if (post == null)
            {
                return NotFound($"Post with id {id} not found.");
            }
            if (!string.IsNullOrEmpty(post.ImageUrl))
            {
                var resultDelete = UploadDelete(post);
                if (!result.IsSuccess)
                {
                    return BadRequest(resultDelete);
                }
            }

            post.ImageUrl = null; // Clear the image URL after deletion
            await _postService.UpdateAsync(post);
            return Ok(Result.Ok(post));
        }

        [HttpPut]
        [Route("UploadUpdate/{id:int}")]
        public async Task<IActionResult> UploadUpdate(int id, IFormFile img)
        {
            if (id <= 0) return BadRequest("Id invalido");
            if (img == null)
            {
                return BadRequest(Result.Fail("a imagem n pode ser nula!"));
            }
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound($"Post with id {id} not found.");
            }
            if (!string.IsNullOrEmpty(post.Value.ImageUrl))
            {
                UploadDelete(post.Value);
            }
        
            var result = await UploudImg(img);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
             

            post.Value.ImageUrl = result.Value;
            var updatedPost = await _postService.UpdateAsync(post.Value);
            if (updatedPost.IsFailed)
            {
                return BadRequest(Result.Fail("Erro ao atualizar post!"));
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostDTO postDto)
        {
            if (postDto == null)
            {
                return BadRequest(Result.Fail("Post cannot be null."));
            }
            Post Post = postDto;
            var createdPost = await _postService.CreateAsync(Post);
            if (createdPost == null)
            {
                return BadRequest(Result.Fail("erro ao criar post!"));
            }
            return CreatedAtAction(nameof(Get), new { id = createdPost.Value.Id }, createdPost);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PostDTO postDto)
        {
            if (postDto == null || postDto.Id != id)
            {
                return BadRequest("Dados invalidos ou Id invalido");
            }
            var updated = await _postService.UpdateAsync(postDto);
            if (updated.IsFailed)
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
            if (deleted.IsFailed)
            {
                return NotFound($"Post with id {id} not found.");
            }
            return Ok($"Post com Id {id} Deletado com sucesso!");
        }
    }
}
