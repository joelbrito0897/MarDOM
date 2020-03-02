using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IWarehouseService : IBaseService<Warehouse>
    {
        Task<int> QuantityAvailable(Guid Id);
        Task<bool> IncreaseCurrentQuantity(Guid Id,int Quantity);
        Task<bool> DecreaseCurrentQuantity(Guid Id, int Quantity);
        Task<bool> AddMovement(MovementArticle data);
        Task<bool> IsAllowedToInsert(int quantity, Guid warehouse);
    }
}
