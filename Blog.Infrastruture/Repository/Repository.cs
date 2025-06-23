using Blog.Infrastruture.Context;
using Blog_Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<T>> GetAsync()
        {
         var content = await _context.Set<T>().AsNoTracking().ToListAsync();

            return content;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entityType = typeof(T);
            var keyProperty = _context.Model.FindEntityType(entityType)?.FindPrimaryKey()?.Properties.FirstOrDefault();
            if (keyProperty == null)
                throw new InvalidOperationException($"A chave primária não foi encontrada para o tipo {entityType.Name}.");

            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, keyProperty.Name);
            var idValue = Expression.Constant(id);
            var equal = Expression.Equal(property, idValue);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            var content = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(lambda);
            if (content == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            return content;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
           var result =  await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<T> CreateAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _context.Set<T>().Remove(await GetByIdAsync(id));
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
