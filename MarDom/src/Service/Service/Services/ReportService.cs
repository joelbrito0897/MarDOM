using Microsoft.EntityFrameworkCore;
using Model;
using Persistence.DatabaseContext;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReportService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MovementArticle>> GetMovementForInOrOut(int inOut)
        {
            var data = await GetMovement()
                   .Where(x => x.Type.Equals(inOut)).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<MovementArticle>> GetMovementForWarehouse(Guid warehouseId)
        {
            var data = await GetMovement()
                .Where(x => x.WarehouseId.Equals(warehouseId)).ToListAsync();
            return data;
        }

        public IQueryable<MovementArticle> GetMovement()
        {
            return _dbContext.MovementArticles
                .Include(x => x.Warehouse)
                .Include(x => x.MovementArticleDetails)
                .Include(x => x.MovementArticleDetails.Article)
                .Include(x => x.MovementArticleDetails.Article.Category).AsQueryable();
        }
    }
}
