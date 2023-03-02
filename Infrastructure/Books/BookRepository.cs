using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Infrastructure.Shared;

namespace miniprojeto_samsys.Infrastructure.Books
{
    public class BookRepository : BaseRepository<Book, BookIsbn>,IBookRepository
    {
        public BookRepository(DDDSample1DbContext context):base(context.Books)
        {
           
        }

        public Task<List<Book>> GetByNameAsync(string bookName)
        {
            throw new NotImplementedException();
        }
    }
}