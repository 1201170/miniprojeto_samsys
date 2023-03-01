using System;
using miniprojeto_samsys.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace miniprojeto_samsys.Domain.Books
{
    public interface IAuthorRepository:IRepository<Book,BookIsbn>
    {
        Task<List<Book>> GetByNameAsync(String bookName);
        
    }
}