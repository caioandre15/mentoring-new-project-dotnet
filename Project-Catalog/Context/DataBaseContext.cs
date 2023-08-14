using ProjectCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectCatalog.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
        {}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProjectCatalog.Models.Attribute> Attributes { get; set; }

    }
}
