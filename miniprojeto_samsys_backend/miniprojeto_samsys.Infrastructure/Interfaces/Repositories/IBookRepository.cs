using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.DAL.Repositories.Shared;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.Infrastructure.Helpers;

namespace miniprojeto_samsys.Infrastructure.Interfaces.Repositories
{
    public interface IBookRepository:IRepository<Book,BookIsbn>
    {
        Task<MessagingHelper<List<Book>>> GetAllBooksAsync();
        Task<MessagingHelper<Book>> GetBookByIdAsync(BookIsbn id, bool includeAuthor = false);
        Task<MessagingHelper<List<Book>>> GetByNameAsync(String bookName);
        Task<PaginatedList<Book>> GetBooks(List<Parameter> filterParameters, List<Parameter> sortingParameters,int currentPage, int pageSize);
        void SoftDeleteBook(Book book);
        Task<int> GetBooksTotalCount();

    }
}