using Common.DbHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class MovementArticleDetails : AuditEntity, ISoftDeleted,IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public int Quantity { get; set; }
        public Guid MovementArticleId { get; set; }
        public virtual MovementArticle MovementArticle { get; set; }
        public bool IsDeleted { get; set; }
    }
}
