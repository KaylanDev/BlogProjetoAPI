using Blog.Application.Interfaces;
using Blog_Domain.Models;
using Blog_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostsRepository _postRepository;

        public PostService(IPostsRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAsync()
        {
            return await _postRepository.GetAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;

            return post;


        }

        public async Task<bool> UpdateAsync(Post entity)
        {
            var result = await _postRepository.UpdateAsync(entity);
            return result;

        }
        public async Task<Post> CreateAsync(Post entity)
        {

            var result = await _postRepository.CreateAsync(entity);

            if (result is null) return null;

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _postRepository.DeleteAsync(id);
            return result;
        }


    }
}
