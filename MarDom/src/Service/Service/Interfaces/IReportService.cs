using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<MovementArticle>> GetMovementForWarehouse(Guid warehouseId);

        Task<IEnumerable<MovementArticle>> GetMovementForInOrOut(int inOut);
    }
}
