using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity>  where TEntity : BaseObject
    {
        internal readonly CompanyContext _dbContext;

        public BaseRepo(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.LastModifiedDate = DateTime.Now;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
