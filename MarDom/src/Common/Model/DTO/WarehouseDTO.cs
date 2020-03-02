using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class WarehouseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int CapacityOfArticlesTotal { get; set; }
    }
}
