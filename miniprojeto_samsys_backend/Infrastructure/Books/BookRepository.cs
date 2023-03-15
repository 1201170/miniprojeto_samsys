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

        public async Task<PaginatedList<Book>> GetBooks(BookParameters bookParameters)
        {
            
            var response = new PaginatedList<Book>();

            try{

            var bookList = await this._objs.Where(b => b.isActive)
            .Include(b => b.Author)
            .OrderBy(b => b.Id)
            .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
            .Take(bookParameters.PageSize)
            .ToListAsync();

            var totalRecords = await this.GetBooksTotalCount();
            response.TotalRecords = totalRecords;

            response.Items = bookList;
            response.CurrentPage = bookParameters.PageNumber;
            response.PageSize = bookParameters.PageSize;

            response.Success = true;
            response.Message = null;


            } catch (Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

    public async Task<MessagingHelper<Book>> GetBookByIdAsync(BookIsbn id, bool includeAuthor = false)
    {

        var response = new MessagingHelper<Book>();

        try{

            var book = new Book();

            if (includeAuthor)
            {
                book = await this._objs
                    .Include(b => b.Author)
                    .Where(b => b.isActive)
                    .SingleOrDefaultAsync(b => b.Id == id);
            }
            else
            {
                book = await this._objs
                    .Where(b => b.isActive)
                    .SingleOrDefaultAsync(b => b.Id == id);
            }

            response.Obj = book;
            response.Success = true;
            response.Message = null;


        } catch (Exception ex){

            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

        public async Task<MessagingHelper<List<Book>>> GetAllBooksAsync (){

            var response = new MessagingHelper<List<Book>>();

            try{

                var bookList = await this._objs.Where(b => b.isActive).ToListAsync();

                response.Obj = bookList;
                response.Success = true;
                response.Message = null;

            } catch (Exception ex) {

                response.Success = false;
                response.Message = ex.Message;

            }

            return response;
        }

        public Task<MessagingHelper<List<Book>>> GetByNameAsync(string bookName)
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