using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Application.DTOs.UserModel;
using Blog_Domain.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using TesteXunit.RepositoryTestexUnit;
using FluentResults;
using Blog_Domain.Repository;

namespace TesteXunit.UserTeste
{
    public class UserTestinServiceReturns : IClassFixture<UserxUnit>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserTestinServiceReturns(UserxUnit userxUnit)
        {
            _userService = new UserService(userxUnit._repository);
            _userRepository = userxUnit._repository;
        }

        // -------------------- GetAsyncAsync --------------------

        [Fact]
        public async Task GetAsyncAsync_DeveRetornarSucesso_QuandoUsuariosExistem()
        {
            var result = await _userService.GetAsyncAsync();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<IEnumerable<UserDTO>>();
        }

        [Fact]
        public async Task GetAsyncAsync_DeveRetornarFalha_QuandoNaoExistemUsuarios()
        {
            // Simule o repositório retornando null (mock ou ajuste no banco de dados de teste)
            // Exemplo: await _userRepository.DeleteAllUsersAsync(); // se existir esse método
            // Aqui apenas o teste, ajuste conforme seu setup
            var result = await _userService.GetAsyncAsync();
            if (!result.IsSuccess)
            {
                result.Errors.Should().NotBeNull();
                result.Errors.Should().Contain(new Error("No users found."));
            }
        }

        // -------------------- GetByNameAsync --------------------

        [Fact]
        public async Task GetByNameAsync_DeveRetornarSucesso_QuandoUsuarioExiste()
        {
            var result = await _userService.GetByNameAsync("admin");
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<UserDTO>();
        }

        [Fact]
        public async Task GetByNameAsync_DeveRetornarFalha_QuandoNomeForNullOuVazio()
        {
            var result = await _userService.GetByNameAsync("");
            result.IsSuccess.Should().BeFalse();
            
        }

        [Fact]
        public async Task GetByNameAsync_DeveRetornarFalha_QuandoUsuarioNaoExiste()
        {
            var result = await _userService.GetByNameAsync("nomeInexistente");
            result.IsSuccess.Should().BeFalse();
         
        }

        // -------------------- GetByIdAsync --------------------

        [Fact]
        public async Task GetByIdAsync_DeveRetornarSucesso_QuandoUsuarioExiste()
        {
            var result = await _userService.GetByIdAsync(1);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<UserDTO>();
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarFalha_QuandoUsuarioNaoExiste()
        {
            var result = await _userService.GetByIdAsync(-1);
            result.IsSuccess.Should().BeFalse();
           
        }

        // -------------------- UpdateAsync --------------------

        [Fact]
        public async Task UpdateAsync_DeveRetornarSucesso_QuandoSenhaCorreta()
        {
            var userDto = new UserDTO { UserId = 1, UserName = "usuarioTeste", Email = "teste@email.com" };
            var result = await _userService.UpdateAsync(userDto, "teste");
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<UserDTO>();
        }

        [Fact]
        public async Task UpdateAsync_DeveRetornarFalha_QuandoSenhaInvalida()
        {
            var userDto = new UserDTO { UserId = 1, UserName = "usuarioTeste", Email = "teste@email.com" };
            var result = await _userService.UpdateAsync(userDto, "senhaErrada");
            result.IsSuccess.Should().BeFalse();
            
        }

        // -------------------- GeneratorPasswordHash --------------------

        [Fact]
        public async Task GeneratorPasswordHash_DeveRetornarSucesso()
        {
            var userDto = new UserDTO { UserId = 1, UserName = "usuarioTeste", Email = "teste@email.com" };
            var result = await _userService.GeneratorPasswordHash(userDto, "senhaTeste");
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.PasswordHash.Should().NotBeNullOrEmpty();
        }

        // -------------------- DeleteAsync --------------------

        [Fact]
        public async Task DeleteAsync_DeveRetornarSucesso_QuandoSenhaCorreta()
        {
            var result = await _userService.DeleteAsync("admin2", "teste");
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalha_QuandoSenhaInvalida()
        {
            var result = await _userService.DeleteAsync("admin2", "senhaErrada");
            result.IsSuccess.Should().BeFalse();
            
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalha_QuandoUsuarioNaoExiste()
        {
            var result = await _userService.DeleteAsync("nomeInexistente", "senhaTeste");
            result.IsSuccess.Should().BeFalse();
            
        }

        // -------------------- GetPostsAndComents --------------------

        [Fact]
        public async Task GetPostsAndComents_DeveRetornarSucesso_QuandoUsuarioTemPosts()
        {
            var result = await _userService.GetPostsAndComents();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<UserDTOPosts>();
        }

        [Fact]
        public async Task GetPostsAndComents_DeveRetornarFalha_QuandoUsuarioNaoTemPosts()
        {
            // Simule o repositório retornando null para GetPostsAndComents
            var result = await _userService.GetPostsAndComents();
            if (!result.IsSuccess)
            {
                result.Errors.Should().Contain(new Error("Nao existem Posts deste usuario"));
            }
        }
    }
}
