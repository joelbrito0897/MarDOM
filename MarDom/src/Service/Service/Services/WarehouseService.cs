using Model;
using Persistence.DatabaseContext;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WarehouseService : BaseService<Warehouse>, IWarehouseService
    {
        private readonly ApplicationDbContext _dbContext;
        public WarehouseService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddMovement(MovementArticle data)
        {
            await _dbContext.Set<MovementArticle>().AddAsync(data);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;

        }

        public async Task<bool> DecreaseCurrentQuantity(Guid Id, int Quantity)
        {
            var warehouse = await GetById(Id);
            warehouse.CurrentQuantity -= Quantity;
            return await Update(warehouse);
        }

        public async Task<bool> IncreaseCurrentQuantity(Guid Id, int Quantity)
        {
            var warehouse = await GetById(Id);
            warehouse.CurrentQuantity += Quantity;
            return await Update(warehouse);
        }

        public async Task<bool> IsAllowedToInsert(int quantity, Guid warehouseId)
        {
            var quantityAvailable = await QuantityAvailable(warehouseId);
            return quantityAvailable > quantity;
        }

        public async Task<int> QuantityAvailable(Guid Id)
        {
            var quantity = await GetById(Id);
            return quantity.QuantityAvailable;
        }

    }
}
