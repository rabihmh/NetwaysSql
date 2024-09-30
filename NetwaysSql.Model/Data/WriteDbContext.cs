namespace NetwaysSql.Model
{
    using Microsoft.EntityFrameworkCore;
    using Netways.Sql.Model;
    using NetwaysSql.Model.EntitiesConfiguration;

    public sealed class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options), IDbContext,IApplicationDbContext
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
        }
        public DbSet<Category> Categories { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
