using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class ArticleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
