using System;
using miniprojeto_samsys.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace miniprojeto_samsys.Domain.Books
{
    public interface IBookRepository:IRepository<Book,BookIsbn>
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(BookIsbn id, bool includeAuthor = false);
        Task<List<Book>> GetByNameAsync(String bookName);
        Task<List<Book>> GetBooks(BookParameters bookParameters);
        void SoftDeleteBook(Book book);
        Task<int> GetBooksTotalCount();

    }
}