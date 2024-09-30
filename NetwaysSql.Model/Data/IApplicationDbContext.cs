
using Microsoft.EntityFrameworkCore;

namespace NetwaysSql.Model
{
    internal interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }

        DbSet<Product> Products { get; }
    }
}
