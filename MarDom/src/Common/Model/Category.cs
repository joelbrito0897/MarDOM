using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Common.DbHelper;

namespace Model
{
    public class Category: AuditEntity, ISoftDeleted,IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
