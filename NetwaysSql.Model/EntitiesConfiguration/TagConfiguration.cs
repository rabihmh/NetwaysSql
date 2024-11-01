namespace NetwaysSql.Model.EntitiesConfiguration
{
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore;
    using System.Reflection.Emit;

  internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
  {
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
        .HasIndex(t => t.Name)
        .IsUnique();

        builder.HasData(
            new Tag { Id = Guid.NewGuid(), Name = "Electronics" },
            new Tag { Id = Guid.NewGuid(), Name = "Home Appliances" },
            new Tag { Id = Guid.NewGuid(), Name = "Fashion" },
            new Tag { Id = Guid.NewGuid(), Name = "Toys" },
            new Tag { Id = Guid.NewGuid(), Name = "Books" },
            new Tag { Id = Guid.NewGuid(), Name = "For Kids" },
            new Tag { Id = Guid.NewGuid(), Name = "Sport" },
            new Tag { Id = Guid.NewGuid(), Name = "Health" }
        );
    }
  }
}
