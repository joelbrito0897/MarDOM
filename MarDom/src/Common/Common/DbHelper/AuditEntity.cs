using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DbHelper
{
    public class AuditEntity
    {
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }
}
