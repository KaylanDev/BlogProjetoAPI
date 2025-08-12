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
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
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
                return Result.Fail("post ou URL da imagem nao existe");
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

        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetById(int postId)
        {
            if (postId <= 0) return BadRequest("Id invalido");
            var post = await _postService.GetByIdAsync(postId);
            if (post.IsFailed)
            {
                return NotFound(post.Errors);
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
           
            if (result.IsFailed)
            {
                return NotFound(result.Errors);
            }
             
            return Ok(result.Value);
        }

        [HttpPost]
        [Route("UploadCreate/{postId:int}")]
        public async Task<IActionResult> UploadCreate(IFormFile img,int postId)
        {
            if (img == null)
            {
                return BadRequest(Result.Fail("a imagem n pode ser nula!"));
            }
            var result = await UploudImg(img);
            var post = await _postService.GetByIdAsync(postId);
            post.Value.ImageUrl = result.Value;
            var updatedPost = await _postService.UpdateAsync(post.Value);
            return Ok(post.Value);
        }

        [HttpDelete]
        [Route("UploadDelete/{postId:int}")]
        public async Task<IActionResult> UploadDelete(int postId)
        {
            if (postId <= 0) return BadRequest("Id invalido");
            var result = await _postService.GetByIdAsync(postId);
            var post = result.Value;
            if (post == null)
            {
                return NotFound($"post with postId {postId} not found.");
            }
            if (!string.IsNullOrEmpty(post.ImageUrl))
            {
                var resultDelete = UploadDelete(post);
                if (result.IsFailed)
                {
                    return BadRequest(resultDelete.Errors);
                }
            }

            post.ImageUrl = null; // Clear the image URL after deletion
            await _postService.UpdateAsync(post);
            return Ok("imagem do post deletada com sucesso!");
        }

        [HttpPut]
        [Route("UploadUpdate/{postId:int}")]
        public async Task<IActionResult> UploadUpdate(int postId, IFormFile img)
        {
            if (postId <= 0) return BadRequest("Id invalido");
            if (img == null)
            {
                return BadRequest(Result.Fail("a imagem n pode ser nula!"));
            }
            var post = await _postService.GetByIdAsync(postId);
            if (post.IsFailed)
            {
                return NotFound(post.Errors);
            }
            if (!string.IsNullOrEmpty(post.Value.ImageUrl))
            {
                UploadDelete(post.Value);
            }
        
            var result = await UploudImg(img);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
             

            post.Value.ImageUrl = result.Value;
            var updatedPost = await _postService.UpdateAsync(post.Value);
            if (updatedPost.IsFailed)
            {
                return BadRequest(updatedPost.Errors);
            }
            return Ok(post.Value);
        }
        //modificar para pegar o user postId do token
        [HttpPost]
        public async Task<IActionResult> Create(PostDTORequest postDto)
        {
            if (postDto == null)
            {
                return BadRequest(Result.Fail("post cannot be null."));
            }
         
            int userId = int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("User ID not found in token."));

            
            var post = new Post
            {
                Content= postDto.Content,
                Title = postDto.Title,

            }
            ;
            post.UserId = userId; // Set the UserId from the authenticated user

            var createdPost = await _postService.CreateAsync(post);
            if (createdPost.IsFailed)
            {
                return BadRequest(createdPost.Errors);
            }
            return CreatedAtAction(nameof(Get), new { id = createdPost.Value.Id }, createdPost.Value);

        }

        [HttpPut("{postId:int}")]
        public async Task<IActionResult> Update(int postId, PostDTORequest postDto)
        {
            if (postDto == null )
            {
                return BadRequest("Dados invalidos ou Id invalido");
            }
            var posrDto = new PostDTO
            {
                Id = postId,
                Title = postDto.Title,
                Content = postDto.Content,
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("User ID not found in token."))
            };
            var updated = await _postService.UpdateAsync(posrDto);
            if (updated.IsFailed)
            {
                return BadRequest(updated.Errors);
            }
            return CreatedAtAction(nameof(GetById), new { postId = posrDto.Id }, postDto);
        }
        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> Delete(int postId)
        {
            if (postId <= 0) return BadRequest("Id invalido");
            var deleted = await _postService.DeleteAsync(postId);
            if (deleted.IsFailed)
            {
                return NotFound(deleted.Errors);
            }
            var deleteImg = await UploadDelete(postId);
            return Ok($"post com Id {postId} Deletado com sucesso!");
        }
    }
}
