using PSchool.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace PSchool.API.DAL.Interfaces
{
    public interface ITenantContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Relationship> Relationships { get; set; }


        void BeginTransaction();
        bool Commit();
        void Rollback();
        Task<bool> SaveChangesAsync();
    }
}
