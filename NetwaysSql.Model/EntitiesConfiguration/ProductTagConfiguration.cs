﻿namespace NetwaysSql.Model.EntitiesConfiguration
{
  using Microsoft.EntityFrameworkCore.Metadata.Builders;
  using Microsoft.EntityFrameworkCore;
    using System.Reflection.Emit;

  internal sealed class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
  {
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
         builder.HasKey(pt => new { pt.ProductId, pt.TagId });

         builder
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId);

        builder
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProductTags)
            .HasForeignKey(pt => pt.TagId);
    }
  }
}
