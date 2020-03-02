using Common.DbHelper;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class MovementArticle : AuditEntity, ISoftDeleted, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int Type { get; set; }
        public bool IsDeleted { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual MovementArticleDetails MovementArticleDetails { get; set; }
        [NotMapped]
        public string InOut => Type.Equals(0) ? "Entrada" : "Salida";
    }
}
