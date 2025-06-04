using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;
        public EfRepositoryBase(TContext context) 
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {   
            var addEntity = _context.Entry(entity);
            addEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            var addEntity = _context.Entry(entity);
            addEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            var updateEntity = _context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var updateEntity = _context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            var deleteEntity = _context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var deleteEntity = _context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public TEntity? Get(
            Expression<Func<TEntity, bool>> predicate,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
                query = include(query);

            if (!tracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
                query = include(query);

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public List<TEntity> GetAll(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
                query = include(query);

            if (!tracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            return query.ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
                query = include(query);

            if (!tracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public TEntity? GetById(
            Guid id,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
                query = include(query);

            if (!tracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<TEntity?> GetByIdAsync(
            Guid id,
            bool tracking = true,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
                query = include(query);

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}