using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Infrastruture.Context;
using Blog.Infrastruture.Repository;
using Blog_Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteXunit.RepositoryTestexUnit
{
  public  class ComentsxUnit : IDisposable
    {
        public readonly IComentsRepository _repository;
        public static DbContextOptions<AppDbContext> contextoptions;
        private AppDbContext Context; // Propriedade estática para acessar o contexto do banco de dados



        private const string conectionString = "Data Source=SHEISLINDA;Initial Catalog=BlogDB;Integrated Security=True;Trust Server Certificate=True";

        static ComentsxUnit()
        {
            contextoptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(conectionString).Options;
        }

        public ComentsxUnit()
        {
            Context = new AppDbContext(contextoptions);
            Context.Database.EnsureDeleted(); // Limpa o banco de dados antes de cada teste
            Context.Database.EnsureCreated(); // Cria o banco de dados antes de cada teste
            _repository = new ComentsRepository(Context);
        }

        public void Dispose()
        {
            Context.Dispose(); // Libera recursos no final da execução de todos os testes da classe
        }
    }
}
