using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PSchool.API.DAL.Entities;
using PSchool.API.DAL.EntityTypeConfigurations;
using PSchool.API.DAL.Interfaces;

namespace PSchool.API.DAL.Contexts
{
    public class TenantContext : DbContext, ITenantContext
    {
        private          IDbContextTransaction _transaction;
        public TenantContext(DbContextOptions<TenantContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        
        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public bool Commit()
        {
            int resultCount;

            try
            {
                resultCount = SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
            }

            return resultCount > 0;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public async Task<bool> SaveChangesAsync()
        {
            int changes = ChangeTracker
                         .Entries()
                         .Count(p => p.State == EntityState.Modified
                                  || p.State == EntityState.Deleted
                                  || p.State == EntityState.Added);

            if (changes == 0) return true;

            return await base.SaveChangesAsync() > 0;
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());                         
            modelBuilder.ApplyConfiguration(new RelationshipConfiguration());                         
        }
    }
}