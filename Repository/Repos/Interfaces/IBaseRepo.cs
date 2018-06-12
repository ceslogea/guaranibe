using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBaseRepo<TEntity>  where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(Guid id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(Guid id);
    }
}