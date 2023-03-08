using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Infrastructure.Shared;

namespace miniprojeto_samsys.Infrastructure.Books
{
    public class BookRepository : BaseRepository<Book, BookIsbn>,IBookRepository
    {

        private readonly DbSet<Book> _objs;

        public BookRepository(DDDSample1DbContext context):base(context.Books)
        {
           this._objs = context.Books;
        }

        public async Task<List<Book>> GetBooks(BookParameters bookParameters)
        {

            return await this._objs
            .OrderBy(b => b.Id)
            .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
            .Take(bookParameters.PageSize)
            .ToListAsync();


            /*
            return PagedList<Book>.ToPagedList(this._objs.OrderBy(on => on.Id),
            bookParameters.PageNumber,
            bookParameters.PageSize);
            */
        }

        public Task<List<Book>> GetByNameAsync(string bookName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetBooksTotalCount(){
            return await this._objs.CountAsync();
        }
    }
}