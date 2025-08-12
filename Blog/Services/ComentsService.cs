using Blog.Application.DTOs.ComentDTOModel;
using Blog.Application.DTOs.Extensions;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public async Task<Result<IEnumerable<ComentsDTO>>> GetAsync()
        {
            var coments = await _comentsRepository.GetAsync();

            if (coments is null) return Result.Fail("Coments not found");

            return Result.Ok(coments.ComentsForDTOLIst());
        }
        public async Task<Result<ComentsDTO>> GetByIdAsync(int id)
        {
            var coment = await _comentsRepository.GetByIdAsync(id);

            if (coment == null) return Result.Fail("Coment not found");
            ComentsDTO comentsDTO = coment;
            return Result.Ok(comentsDTO);
        }
        public async Task<Result<ComentsDTO>> CreateAsync(ComentsDTO entity)
        {
            if (entity is null ) return Result.Fail("Coments cannot be null! ");
            Coment coment = entity;
            var result = await _comentsRepository.CreateAsync(coment);
            entity = result;
            return Result.Ok(entity);
        }
        public async Task<Result> UpdateAsync(ComentsDTO entity)
        {
            if (entity == null) return Result.Fail("Coment cannot be null");

            var existingComent = await _comentsRepository.GetByIdAsync(entity.ComentId);
            if (existingComent == null) return Result.Fail("Coment not found"); 

            existingComent.Content = entity.Content;

            var result = await _comentsRepository.UpdateAsync(existingComent);
            return Result.OkIf(result,"failed update coment");
        }
        public async Task<Result> DeleteAsync(int ComentId,int userId)
        {
            if (ComentId <= 0 || userId <= 0) return Result.Fail("ComentId cannot be 0 or negative");

            var existingComent = await _comentsRepository.GetByIdAsync(ComentId);

            if (existingComent == null) return Result.Fail("Coment not found");
            if (userId != existingComent.UserId) return Result.Fail("User not authorized to delete this coment");

            var result = await _comentsRepository.DeleteAsync(ComentId);
            return Result.OkIf(result,"failed delete coment");
        }




    }
}
