using Microsoft.EntityFrameworkCore;

namespace miniprojeto_samsys.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {

        /*
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Family> Families { get; set; }

        */

        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new FamilyEntityTypeConfiguration());
        }
    }
}