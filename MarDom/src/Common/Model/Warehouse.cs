using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common.DbHelper;

namespace Model
{
    public class Warehouse : AuditEntity, ISoftDeleted, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [StringLength(300)]
        [DisplayName("Direccion")]
        public string Address { get; set; }

        [StringLength(1000)]
        [DisplayName("Descripcion")]
        public string Description { get; set; }

        [DisplayName("Capacidad")]
        public int CapacityOfArticlesTotal { get; set; }

        [DisplayName("Cantidad Actual")]
        public int CurrentQuantity { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        [NotMapped]

        [DisplayName("Cantidad Disponible")]
        public virtual int QuantityAvailable => CapacityOfArticlesTotal - CurrentQuantity;

       // public virtual MovementArticle MovementArticle { get; set; }

    }
}
