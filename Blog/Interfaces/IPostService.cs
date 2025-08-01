using Blog.Application.DTOs.PostsDTOModel;
using Blog_Domain.Models;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces;

public  interface IPostService
{
    Task<Result<IEnumerable<PostDTO>>> GetAsync();  
    Task<Result<IEnumerable<PostDTO>>> GetPostByTittle(string tittle);
    Task<Result<PostDTO>> GetByIdAsync(int id);
    Task<Result<PostDTO>> CreateAsync(PostDTO entity);
    Task<Result> UpdateAsync(PostDTO entity);
    Task<Result> DeleteAsync(int id);
}
