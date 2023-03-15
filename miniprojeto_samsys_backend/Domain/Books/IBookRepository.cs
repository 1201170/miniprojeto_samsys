using System;
using miniprojeto_samsys.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.Infrastructure;


namespace miniprojeto_samsys.Domain.Books
{
    public interface IBookRepository:IRepository<Book,BookIsbn>
    {
        Task<MessagingHelper<List<Book>>> GetAllBooksAsync();
        Task<MessagingHelper<Book>> GetBookByIdAsync(BookIsbn id, bool includeAuthor = false);
        Task<MessagingHelper<List<Book>>> GetByNameAsync(String bookName);
        Task<PaginatedList<Book>> GetBooks(BookParameters bookParameters);
        void SoftDeleteBook(Book book);
        Task<int> GetBooksTotalCount();

    }
}