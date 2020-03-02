using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence.DatabaseContext;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ArticleService : BaseService<Article>, IArticleService
    {
        private readonly ApplicationDbContext _dbContext;
        public ArticleService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Article model)
        {
            var result = await Add(model);
            return result;
        }

        public IQueryable<Article> GetList()
        {
            return _dbContext.Set<Article>().Include(x => x.Category).AsQueryable();
        }
    }
}
