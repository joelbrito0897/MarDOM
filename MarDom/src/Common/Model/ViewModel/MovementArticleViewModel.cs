using Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class MovementArticleViewModel
    {
        public int MovementType { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid ArticleId { get; set; }
        public int Quantity { get; set; }
    }
}
