using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Domain.Authors;



namespace miniprojeto_samsys.Infrastructure.Books
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.ToTable("book");
            builder.HasKey(b => b.Id);
            //builder.HasOne<Author>(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.BookAuthorID);
            builder.OwnsOne(o => o.BookName);
            //builder.OwnsOne(b => b.BookName);
            builder.OwnsOne(o => o.BookPrice);
            builder.Property<bool>("isActive").HasColumnName("isActive");
            //builder.Property<double>("BookPrice").HasColumnName("BookPrice").IsRequired();

        }
    }
}