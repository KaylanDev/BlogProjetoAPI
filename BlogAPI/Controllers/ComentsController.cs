using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentsController : ControllerBase
{
    private readonly IComentsService _comentsService;

    public ComentsController(IComentsService comentsService)
    {
        _comentsService = comentsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var coments = await _comentsService.GetAsync();
        if (coments == null || !coments.Any())
        {
            return NotFound("No comments found.");
        }
        return Ok(coments);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Id invalido");
        var coment = await _comentsService.GetByIdAsync(id);
        if (coment == null)
        {
            return NotFound($"Comentario com  id {id} nao encontrado.");
        }
        return Ok(coment);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ComentsDTO comentDto)
    {
        if (comentDto == null)
        {
            return BadRequest("Coment nao pode ser null.");
        }
        var createdComent = await _comentsService.CreateAsync(comentDto);
        if (createdComent == null)
        {
            return BadRequest("falha ao criar comentario.");
        }
        return CreatedAtAction(nameof(GetById), new { id = createdComent.ComentId }, createdComent);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id,ComentsDTO comentDto)
    {
        if (comentDto == null|| id != comentDto.ComentId)
        {
            return BadRequest("Dados invalidos");
        }
        var updated = await _comentsService.UpdateAsync(comentDto);
        if (!updated)
        {
            return BadRequest("falha ao atualizar comentario.");
        }
        return CreatedAtAction(nameof(GetById), new { id = comentDto.ComentId }, comentDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Id invalido");
        var deleted = await _comentsService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound($"Comentario com id {id} nao encontrado.");
        }
        return Ok("Comentario excluido com sucesso!");
    }
}
