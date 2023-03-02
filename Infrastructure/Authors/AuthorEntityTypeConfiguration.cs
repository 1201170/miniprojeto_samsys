using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Domain.Authors;



namespace miniprojeto_samsys.Infrastructure.Authors
{
    internal class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //builder.ToTable("author");
            builder.HasKey(a => a.Id);
            //builder.HasMany<Book>(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.BookAuthorID);
            //builder.Property<string>("AuthorName").HasColumnName("AuthorName").IsRequired();
            builder.OwnsOne(a => a.AuthorName);
        }
    }
}