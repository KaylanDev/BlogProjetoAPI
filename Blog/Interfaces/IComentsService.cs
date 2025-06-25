using Blog.Application.DTOs;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
  public  interface IComentsService 
    {
        Task<IEnumerable<ComentsDTO>> GetAsync();
        Task<ComentsDTO> GetByIdAsync(int id);
        Task<ComentsDTO> CreateAsync(ComentsDTO entity);
        Task<bool> UpdateAsync(ComentsDTO entity);
        Task<bool> DeleteAsync(int id);


    }
}
