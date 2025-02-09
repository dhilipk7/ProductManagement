using Microsoft.EntityFrameworkCore;
using ProductManagement.Data.Entities;

namespace ProductManagement.Data.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
