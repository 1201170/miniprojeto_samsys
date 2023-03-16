using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using miniprojeto_samsys.Infrastructure.Entities.Books;

namespace miniprojeto_samsys.DAL.Repositories.Books
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.ToTable("book");
            builder.HasKey(b => b.Id);
            builder.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.BookAuthorID);
;
            builder.OwnsOne(o => o.BookName);
            builder.OwnsOne(o => o.BookPrice);
            builder.Property<bool>("isActive").HasColumnName("isActive");

        }
    }
}