using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.DbHelper;
using Model;

namespace Persistence.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //DbSets

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<MovementArticle> MovementArticles { get; set; }
        public DbSet<MovementArticleDetails> MovementArticleDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            AddMyFilters(ref builder);
            base.OnModelCreating(builder);
        }

        #region OverrideSaveChangesMethods
        public override int SaveChanges()
        {
            MakeAuditit();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            MakeAuditit();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            MakeAuditit();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            MakeAuditit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion

        #region Audit
        private void MakeAuditit()
        {
            var changEntity = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity
                     && (x.State == EntityState.Added
                         || x.State == EntityState.Modified)
            );

            foreach (var entry in changEntity)
            {
                if (entry.Entity is AuditEntity entity)
                {
                    var date = DateTime.Now;
                    var userId = "SYSTEM";

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreateAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).IsDeleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }
                    else if (entry.State == EntityState.Modified && !((ISoftDeleted)entity).IsDeleted)
                    {
                        entity.UpdatedAt = date;
                        entity.UpdatedBy = userId;
                    }

                }
            }
        }
        #endregion

        #region Filters
        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Article>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<MovementArticle>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

            modelBuilder.Entity<Warehouse>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Article>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<MovementArticle>().HasQueryFilter(x => !x.IsDeleted);

        }
        #endregion

    }
}
