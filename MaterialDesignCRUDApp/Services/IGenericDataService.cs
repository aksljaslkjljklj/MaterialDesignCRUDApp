using MaterialDesignCRUDApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.Services
{
    public interface IGenericDataService<TEntity> where TEntity : DomainObject
    {
        Task<int> AddAsync(TEntity entity);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<int> RemoveAsync(int id);
        Task<int> UpdateAsync(int id, TEntity entity);
    }
}