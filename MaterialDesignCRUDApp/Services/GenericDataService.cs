using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.SQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.Services
{
    public class GenericDataService<TEntity> : IGenericDataService<TEntity> where TEntity : DomainObject
    {
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            using (var ctx = new SqlDataContext())
            {
                await ctx.Set<TEntity>().AddAsync(entity);
                return await ctx.SaveChangesAsync();
            }
        }

        public virtual async Task<int> UpdateAsync(int id, TEntity entity)
        {
            using (var ctx = new SqlDataContext())
            {
                entity.Id = id;
                TEntity oldEntity = await ctx.FindAsync<TEntity>(id);
                if (entity == null)
                    return await Task.FromResult(0);
                ctx.Entry(oldEntity).CurrentValues.SetValues(entity);
                return await ctx.SaveChangesAsync();
            }
        }

        public virtual async Task<int> RemoveAsync(int id)
        {
            using (var ctx = new SqlDataContext())
            {
                TEntity entity = await ctx.FindAsync<TEntity>(id);
                if (entity == null)
                    return await Task.FromResult(0);
                ctx.Set<TEntity>().Remove(entity);
                return await ctx.SaveChangesAsync();
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            using (var ctx = new SqlDataContext())
            {
                return await ctx.FindAsync<TEntity>(id);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync()
        {
            using (var ctx = new SqlDataContext())
            {
                return await ctx.Set<TEntity>().ToListAsync();
            }
        }
    }
}
