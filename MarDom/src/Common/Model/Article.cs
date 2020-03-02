using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Common.DbHelper;
using Microsoft.AspNetCore.Http;

namespace Model
{
    public class Article : AuditEntity,ISoftDeleted,IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Categoria")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Precio")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
