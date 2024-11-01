namespace NetwaysSql.Model
{
    using Microsoft.EntityFrameworkCore;
    using Netways.Sql.Model;
    using NetwaysSql.Model.EntitiesConfiguration;

    public sealed class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options), IDbContext,IApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies(false);

            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new TagConfiguration());

            modelBuilder.ApplyConfiguration(new ProductTagConfiguration());
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
