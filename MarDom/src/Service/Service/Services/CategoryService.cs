using Microsoft.EntityFrameworkCore;
using Model;
using Persistence.DatabaseContext;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetList()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
