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

            return await this._objs.Where(b => b.isActive)
            .OrderBy(b => b.Id)
            .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
            .Take(bookParameters.PageSize)
            .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(BookIsbn id){
            return await this._objs.Where(b => id.Equals(b.Id)).Where(b => b.isActive).FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync (){
            return await this._objs.Where(b => b.isActive).ToListAsync();
        }

        public Task<List<Book>> GetByNameAsync(string bookName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetBooksTotalCount(){
            return await this._objs.Where(b => b.isActive).CountAsync();
        }

        public void SoftDeleteBook(Book book){
            book.isActive=false;
        }
    }
}