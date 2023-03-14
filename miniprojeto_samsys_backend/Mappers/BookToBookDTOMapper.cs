using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Domain.Authors;


namespace miniprojeto_samsys.Mappers{

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