namespace NetwaysSql.Model.EntitiesConfiguration
{
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore;

  internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Description).HasMaxLength(250);

        builder.Property(u => u.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Category) // Product has one Category
                   .WithMany() // Category can have many Products
                   .HasForeignKey(p => p.CategoryId) // Foreign key in Product
                   .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.CategoryId)
                .IsRequired(); 
    }
  }
}
