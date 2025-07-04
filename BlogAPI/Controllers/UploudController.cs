using Blog.Application.DTOs.PostsDTOModel;
using Blog_Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploudController : ControllerBase
{
    private readonly IWebHostEnvironment _hostingEnvironment;
  

    public UploudController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
      
    }

    // Modelo para receber dados do POST, incluindo o arquivo


    [HttpPost]
    public async Task<IActionResult> CreatePost( IFormFile img)
    {
        if (img == null)
        {
            return BadRequest("Dados do post inválidos.");
        }
        
        string imageUrl = null;

        if (img != null && img.Length > 0)
        {
            // Validação do tipo de arquivo (opcional, mas recomendado)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(img.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Tipo de arquivo não permitido. Apenas JPG, JPEG, PNG e GIF são aceitos.");
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

      



        return Ok(new
        {
            Message = "Post criado com sucesso!",
            ImageUrl = imageUrl
        });
    }




}
