using Blog_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs;

public class ComentsDTO
{
    public int ComentId { get; set; }
    public string Content { get; set; }
    public int? PostId { get; set; }
    public int UserId { get; set; }


    public static implicit operator ComentsDTO(Coment coment)
    {
        return new ComentsDTO
        {
            ComentId = coment.ComentId,
            Content = coment.Content,
            PostId = coment.PostId,
            UserId = coment.UserId
        };
    }

    public static implicit operator Coment(ComentsDTO comentDto)
    {
        return new Blog_Domain.Models.Coment
        {
            ComentId = comentDto.ComentId,
            Content = comentDto.Content,
            PostId = comentDto.PostId,
            UserId = comentDto.UserId
        };
    }


}
