using System;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Collections.Generic;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Books
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _repo;
        private readonly IAuthorRepository _repoAuthor;

        public BookService(IUnitOfWork unitOfWork, IBookRepository repo, IAuthorRepository repoAuthor)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoAuthor = repoAuthor;
        }

        public BookService()
        {
            //ensure utility
        }

        public async Task<List<BookDTO>> GetAllAsync (){

            Console.WriteLine("Fetching all books");

            var list = await this._repo.GetAllAsync();

            List<BookDTO> listDTO = list.ConvertAll<BookDTO>(book => new BookDTO{bookIsbn = book.Id.AsString(), 
                                    bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice });

            return listDTO;
        }


        public async Task<List<BookDTO>> GetBooksAsync (BookParameters bookParameters){

            Console.WriteLine("Fetching books parameters are: Page Size: " +bookParameters.PageSize+ " | Page Number: "+ bookParameters.PageNumber);

            var list = await this._repo.GetBooks(bookParameters);

            List<BookDTO> listDTO = list.ConvertAll<BookDTO>(book => new BookDTO{bookIsbn = book.Id.AsString(), 
                                    bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice });

            return listDTO;
        }



        public async Task<BookDTO> GetByIdAsync (BookIsbn id){

            Console.WriteLine("Fetching book with id: "+id.AsString());

            var book = await this._repo.GetByIdAsync(id);

            if (book == null)
                return null;

            return new BookDTO{bookIsbn = book.Id.AsString(), bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice};
        }

        public async Task<BookDTO> AddAsync(BookDTO dto){

            Console.WriteLine("Adding book");
            
            await checkAuthorIdAsync(new AuthorId(dto.bookAuthor));

            var book = new Book(dto.bookIsbn, dto.bookName, dto.bookPrice, new AuthorId(dto.bookAuthor));

            await this._repo.AddAsync(book);

            await this._unitOfWork.CommitAsync();

            return new BookDTO{bookIsbn = book.Id.AsString(), bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice};
        }

        public async Task<BookDTO> DeleteAsync(BookIsbn id){

            Console.WriteLine("Deleting book with id: "+id.AsString());

            var book = await this._repo.GetByIdAsync(id); 

            if (book == null)
                return null;

            this._repo.Remove(book);
            await this._unitOfWork.CommitAsync();

            return new BookDTO{bookIsbn = book.Id.AsString(), bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice};

        }

        private async Task checkAuthorIdAsync(AuthorId authorId)
        {
           var author = await _repoAuthor.GetByIdAsync(authorId);
           if (author == null)
                throw new BusinessRuleValidationException("Invalid Author Id.");
        }
        



    }
}