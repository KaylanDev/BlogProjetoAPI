using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Domain.Repository
{
    public interface IRepository<T> where T : class
    {
       Task<IEnumerable<T>> GetAsync();
       Task<T> GetByIdAsync(int id);
       Task<T> CreateAsync(T entity);
       Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
