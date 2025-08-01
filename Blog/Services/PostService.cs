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
using Blog.Application.DTOs.PostsDTOModel;
using Microsoft.AspNetCore.Http;
using FluentResults;

namespace Blog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostsRepository _postRepository;


        public PostService(IPostsRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Result<IEnumerable<PostDTO>>> GetAsync()
        {
            var posts = await _postRepository.GetAsync();
            if (posts is null) return Result.Fail("Posts Not found");
            var postDTOs = posts.PostsForDTOLIst();
            return Result.Ok(postDTOs);
        }


        public async Task<Result<PostDTO>> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) Result.Fail("Posts Not found");
            PostDTO postDTO = post;
            return Result.Ok(postDTO);


        }
        public async Task<Result<IEnumerable<PostDTO>>> GetPostByTittle(string tittle)
        {
            var posts = await _postRepository.GetPostByTittleAsync(tittle);
            if (posts == null) return Result.Fail("N foi encontrado Posts com esse titulo");
            var postDTOs = posts.PostsForDTOLIst();
            return Result.Ok(postDTOs);
        }

        public async Task<Result> UpdateAsync(PostDTO entity)
        {
            if (entity == null) return Result.Fail("Post não pode ser nulo!");
            var existingPost = await _postRepository.GetByIdAsync(entity.Id);
            if (existingPost == null) return Result.Fail("Post not found");
            existingPost = entity;
            var result = await _postRepository.UpdateAsync(existingPost);
            if (!result) return Result.Fail("Failed to update post");
            return Result.Ok();

        }
        public async Task<Result<PostDTO>> CreateAsync(PostDTO entity)
        {
            if (entity == null) return Result.Fail("Post não pode ser nulo!");
            Post post = entity;
            var result = await _postRepository.CreateAsync(post);
            if (result == null) return Result.Fail("Failed to create post");
            PostDTO postDto = result;
            return Result.Ok(postDto);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);
            if (existingPost == null) return Result.Fail("Post not found");
            var result = await _postRepository.DeleteAsync(existingPost);
            if (!result) return Result.Fail("Failed to delete post");
            return Result.Ok();
        }

        
    }
}
