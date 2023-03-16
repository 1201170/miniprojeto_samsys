using Microsoft.EntityFrameworkCore;
using miniprojeto_samsys.DAL.Repositories.Authors;
using miniprojeto_samsys.DAL.Repositories.Books;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Books;

namespace miniprojeto_samsys.DAL
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