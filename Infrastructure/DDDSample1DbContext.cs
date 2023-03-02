using Microsoft.EntityFrameworkCore;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Infrastructure.Books;
using miniprojeto_samsys.Infrastructure.Authors;


namespace miniprojeto_samsys.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {

        
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
        

        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
        }
    }
}