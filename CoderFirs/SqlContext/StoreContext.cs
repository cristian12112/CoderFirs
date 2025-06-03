using CoderFirs.Models;
using Microsoft.EntityFrameworkCore;

namespace CoderFirs.SqlContext
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<BeerModels> Beers { get; set; }
        public DbSet<BrandModels> Brands { get; set; }
    }
}
