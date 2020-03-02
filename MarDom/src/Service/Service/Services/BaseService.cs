using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DbHelper;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Service.Interfaces;


namespace Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> 
        where TEntity:class, IEntity, ISoftDeleted
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result.Equals(1);
        }

        public async Task<bool> Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); 
            var result = await _dbContext.SaveChangesAsync();
            return result.Equals(1);
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result.Equals(1);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking(); 
        }

        public async Task<bool> Disable(Guid Id)
        {
            var entry = await GetById(Id);
            entry.IsDeleted = true;
            return await Update(entry);
        }
    }
}
