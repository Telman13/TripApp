using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity> 
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        TEntity? Get(
            Expression<Func<TEntity, bool>> predicate,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        List<TEntity> GetAll(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        TEntity? GetById(
            Guid id,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        Task<TEntity?> GetByIdAsync(
            Guid id,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    }
}
