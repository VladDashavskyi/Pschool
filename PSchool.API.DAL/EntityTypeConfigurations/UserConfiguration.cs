using PSchool.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;


namespace PSchool.API.DAL.EntityTypeConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("Users", "dbo");
            builder
               .HasKey(e => e.UserId);

            builder
               .Property(e => e.FirstName)
               .HasMaxLength(50);
            
            builder
               .Property(e => e.LastName)
               .HasMaxLength(50);
            
            builder
               .Property(e => e.Email)
               .HasMaxLength(150);
            
   
        }
    }
}