using Microsoft.EntityFrameworkCore;
using Crud_Rest_API_Using_DotNet.Model;

namespace Crud_Rest_API_Using_DotNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


    }
}
