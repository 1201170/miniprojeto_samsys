
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.Infrastructure.Models.Books;

namespace miniprojeto_samsys.BLL.Mappers{

    public class BookToBookDTOMapper {

        public static BookDTO ToBookDTOMap(Book book){

            return new BookDTO()
            {
                bookIsbn = book.Id.AsString(),
                bookAuthor = book.BookAuthorID.AsString(),
                bookName = book.BookName._BookName,
                bookPrice = book.BookPrice._BookPrice
            };

        }

        
        public static Book ToBookMap(BookDTO bookDTO){

            return new Book(bookDTO.bookIsbn, bookDTO.bookName, bookDTO.bookPrice, new AuthorId(bookDTO.bookAuthor));

        }

    }
}