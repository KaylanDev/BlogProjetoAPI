using Blog.Application.DTOs.PostsDTOModel;
using Blog_Domain.Models;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
 public  interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAsync();
        Task<PostDTO> GetByIdAsync(int id);
        Task<PostDTO> CreateAsync(PostDTO entity);

        Task<Result<bool>> UpdateAsync(PostDTO entity);
        Task<Result<bool>> DeleteAsync(int id);
        Task<IEnumerable<PostDTO>> GetPostByTittle(string tittle);



    }
}
