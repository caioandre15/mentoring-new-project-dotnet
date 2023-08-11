using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
        {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Models.Attribute> Attributes { get; set; }

    }
}
