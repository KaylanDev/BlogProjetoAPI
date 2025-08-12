using Blog.Application.DTOs.ComentDTOModel;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace Blog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ComentsController : ControllerBase
{
    private readonly IComentsService _comentsService;
    private readonly IPostService _postService;
    public ComentsController(IComentsService comentsService, IPostService postService)
    {
        _comentsService = comentsService;
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var coments = await _comentsService.GetAsync();
        if (coments == null || !coments.Value.Any())
        {
            return NotFound("No comments found.");
        }
        return Ok(coments.Value);
    }

    [HttpGet("{ComentId:int}")]
    public async Task<IActionResult> GetById(int ComentId)
    {
        if (ComentId <= 0) return BadRequest("Id invalido");
        var coment = await _comentsService.GetByIdAsync(ComentId);
        if (coment == null)
        {
            return NotFound($"Comentario com  ComentId {ComentId} nao encontrado.");
        }
        return Ok(coment.Value);
    }

    [HttpPost("{postId:int}")]
    public async Task<IActionResult> Post(int postId,ComentsDTO comentDto)
    {
        if (comentDto == null) return BadRequest("Coment nao pode ser null.");
    
        if (comentDto.PostId <= 0) return BadRequest("PostId invalido.");

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("User ID not found in token."));
        if (comentDto.UserId != userId) return BadRequest("Usuario nao autorizado a criar comentario.");

        var postexists = await _postService.GetByIdAsync(postId);
        if (postexists.IsFailed)
        {
            return NotFound($"Post com ComentId {postId} nao encontrado.");
        }

        var createdComent = await _comentsService.CreateAsync(comentDto);
        if (createdComent.IsFailed) return BadRequest("falha ao criar comentario.");
    
        return CreatedAtAction(nameof(GetById), new { ComentId = createdComent.Value.ComentId }, createdComent.Value);
    }

    [HttpPut("{ComentId:int}")]
    public async Task<IActionResult> Put(int ComentId,ComentDTOUpdate comentDto)
    {
        if (comentDto == null)
        {
            return BadRequest("Dados invalidos");
        }

        var coment = new ComentsDTO
        {
            Content = comentDto.Content,
            ComentId = ComentId,
        };
        var updated = await _comentsService.UpdateAsync(coment);
        if (updated.IsFailed)
        {
            return BadRequest("falha ao atualizar comentario.");
        }
        return CreatedAtAction(nameof(GetById), new { ComentId = coment.ComentId }, comentDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Id invalido");
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("User ID not found in token."));
        var deleted = await _comentsService.DeleteAsync(id,userId);
        if (deleted.IsFailed)
        {
            return NotFound(deleted.Errors);
        }
        return Ok("Comentario excluido com sucesso!");
    }
}
