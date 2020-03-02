using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DbHelper;

namespace Service.Interfaces
{
    public interface IBaseService<TEntity> 
        where TEntity : class ,IEntity, ISoftDeleted
    {
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<TEntity> GetById(Guid id);
        IQueryable<TEntity> GetAll();
        Task<bool> Disable(Guid id);
    }
}
