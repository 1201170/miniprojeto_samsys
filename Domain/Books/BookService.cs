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
            var list = await this._repo.GetAllAsync();

            List<BookDTO> listDTO = list.ConvertAll<BookDTO>(book => new BookDTO{bookIsbn = book.Id.AsInteger(), 
                                    bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice });

            return listDTO;
        }

        public async Task<BookDTO> GetByIdAsync (BookIsbn id){

            var book = await this._repo.GetByIdAsync(id);

            if (book == null)
                return null;

            return new BookDTO{bookIsbn = book.Id.AsInteger(), bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice};
        }


    }
}