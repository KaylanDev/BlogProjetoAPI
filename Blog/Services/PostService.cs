using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using Blog.Application.DTOs.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace Blog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostsRepository _postRepository;

        public PostService(IPostsRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDTO>> GetAsync()
        {
            var posts = await _postRepository.GetAsync();
            var postDTOs = posts.PostsForDTOLIst();
            return postDTOs;
        }


        public async Task<PostDTO> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;

            return post;


        }

        public async Task<bool> UpdateAsync(PostDTO entity)
        {
            if (entity == null) return false;
            var existingPost = await _postRepository.GetByIdAsync(entity.Id);
            if (existingPost == null) return false;
            existingPost = entity;
            var result = await _postRepository.UpdateAsync(existingPost);
            return result;

        }
        public async Task<PostDTO> CreateAsync(PostDTO entity)
        {
            if (entity == null) return null;

            Post post = entity;
            var result = await _postRepository.UpdateAsync(post);
            return post;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _postRepository.DeleteAsync(id);
            return result;
        }


    }
}
