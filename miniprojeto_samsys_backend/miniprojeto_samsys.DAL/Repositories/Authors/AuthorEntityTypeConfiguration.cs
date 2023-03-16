using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using miniprojeto_samsys.Infrastructure.Entities.Authors;

namespace miniprojeto_samsys.DAL.Repositories.Authors
{
    internal class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //builder.ToTable("author");
            builder.HasKey(a => a.Id);
            //builder.HasMany<Book>(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.BookAuthorID).OnDelete(DeleteBehavior.Cascade);
            builder.OwnsOne(a => a.AuthorName);
        }
    }
}