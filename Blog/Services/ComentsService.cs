using Blog.Application.DTOs;
using Blog.Application.DTOs.Extensions;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class ComentsService : IComentsService
    {
        private readonly IComentsRepository _comentsRepository;

        public ComentsService(IComentsRepository comentsRepository)
        {
            _comentsRepository = comentsRepository;
        }
        public async Task<IEnumerable<ComentsDTO>> GetAsync()
        {
            var coments = await _comentsRepository.GetAsync();

            return coments.ComentsForDTOLIst();
        }
        public async Task<ComentsDTO> GetByIdAsync(int id)
        {
            var coment = await _comentsRepository.GetByIdAsync(id);

            if (coment == null) return null;
            return coment;
        }
        public async Task<ComentsDTO> CreateAsync(ComentsDTO entity)
        {
            if (entity == null) return null;
            Coment coment = entity;
            var result = await _comentsRepository.CreateAsync(coment);
            return result;
        }
  public async Task<bool> UpdateAsync(ComentsDTO entity)
        {
            if (entity == null) return false;
            var existingComent = await _comentsRepository.GetByIdAsync(entity.ComentId);
            if (existingComent == null) return false;
            existingComent = entity;
            var result = await _comentsRepository.UpdateAsync(existingComent);
            return result;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;
            var result = await _comentsRepository.DeleteAsync(id);
            return result;
        }



      
    }
}
