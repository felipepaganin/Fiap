using Fiap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Data.Mapping
{
    public class ClassMap : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Turma");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClassName)
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(x => x.Year)
                .HasMaxLength(4)
                .IsRequired();
        }
    }
}