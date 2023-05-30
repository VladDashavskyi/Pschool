using PSchool.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PSchool.API.DAL.EntityTypeConfigurations
{
    class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder
               .ToTable("Relationship", "dbo");
            builder
               .HasIndex(e => new {e.Id, e.ParentId })
               .IsUnique();
            builder
                .HasOne<User>()
                .WithOne(w => w.Relationship).HasForeignKey<Relationship>( r => r.Id);
        }
    }
}