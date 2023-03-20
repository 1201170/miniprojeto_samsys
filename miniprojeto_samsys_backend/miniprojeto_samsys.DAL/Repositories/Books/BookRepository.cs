using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using miniprojeto_samsys.DAL.Repositories.Shared;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.Infrastructure.Helpers;
using miniprojeto_samsys.Infrastructure.Interfaces.Repositories;

namespace miniprojeto_samsys.DAL.Repositories.Books
{
    public class BookRepository : BaseRepository<Book, BookIsbn>,IBookRepository
    {

        private readonly DbSet<Book> _objs;

        public BookRepository(DDDSample1DbContext context):base(context.Books)
        {
           this._objs = context.Books;
        }

        public async Task<PaginatedList<Book>> GetBooks(List<Parameter> filterParameters, List<Parameter> sortingParameters,int currentPage, int pageSize)
        {
            
            PaginatedList<Book> response = new PaginatedList<Book>();

            try{

                filterParameters = Parameter.LoadFrom(new string[] {
                "bookIsbn","bookAuthor", "bookName", "bookPrice"}, filterParameters, null);

            var query = _objs.Include(x => x.Author).Where(b => b.isActive).AsQueryable<Book>();


            //Filtering with parameters
            if (filterParameters.Count() > 0)
            {
                foreach (var parameter in filterParameters)
                {
                    if (parameter.Value != null)
                    {

                        switch (parameter.Name)
                        {
                            case "bookIsbn":
                                //If you found this then you came for copper and found gold
                                //I will now explain this absolute code abomination.
                                //The query below this comment should not exist
                                //However circunstances forced me to destroy the compiler logic
                                //As the .Contains Method can only be applied to a string why didnt i apply it in the id?
                                //Well the Id is a complicated object because its value is derived from the EntityID class
                                //And to use its value as a string i need to call the AsString() method which is not
                                //supported by LINQ queries and thus cannot be used with the AsQueryable

                                //Developers Note: It can be used with AsEnumerable however it has some drawbacks that
                                //I didnt like such as bringing ALL data from the database and then applying whatever you
                                //needed to the objects (Not as efficient as AsQueryable). 

                                //So how do we solve this problem with the AsString() and the .Contains with AsQueryable?
                                //We dont
                                //Instead we call EF.Functions that provides CLR methods that get translated
                                //to database functions when used in LINQ to Entities queries.
                                //Now that FTS (Full text search) was needed i opted to use the Like function
                                //If you dont know what the % in $"%{parameter.Value}%" is go google it
                                //Now, this | (string)(object)b.Id | thing is a very nice compiler "hack" as it
                                //casts the Id (wich is a BookIsbn class) to an object to fool the compiler and then 
                                // to a string that can be used in the function (the AsString() method does not work)
                                //this way either and thats why the double cast is necessary.

                                //To put it in another words : In this case, you know that the CLR equivalent of the provider type is string
                                // so you can use the following cast.

                                //The intermediate cast to object is needed to fool C# compiler to
                                //accept the actual cast. EF Core translator is smart enough to remove it (as well as others when not needed like here).

                                query = query.Where(b => EF.Functions.Like((string)(object)b.Id, $"%{parameter.Value}%"));
                                break;
                            case "bookAuthor":
                                query = query.Where(e => e.Author.AuthorName._AuthorName.Contains(parameter.Value));
                                break;
                            case "bookName":
                                query = query.Where(e => e.BookName._BookName.Contains(parameter.Value));
                                break;
                            case "bookPrice":
                                query = query.Where(e => e.BookPrice._BookPrice.Contains(parameter.Value));
                                break;
                        }
                    }
                }
            }


            //Sorting when at least one sorting parameter is present
            if (sortingParameters != null && sortingParameters.Count > 0)
            {

                switch (sortingParameters[0].Name)
                {
                    case "bookIsbn":
                        query = sortingParameters[0].Value == "DESC" ? query.OrderByDescending(a => (string)(object)a.Id) : query.OrderBy(a => (string)(object)a.Id);
                        break;

                    case "bookAuthor":
                        query = sortingParameters[0].Value == "DESC" ? query.OrderByDescending(a => a.Author.AuthorName._AuthorName) : query.OrderBy(a => a.Author.AuthorName._AuthorName);
                        break;
                    case "bookName":
                        query = sortingParameters[0].Value == "DESC" ? query.OrderByDescending(a => a.BookName._BookName) : query.OrderBy(a => a.BookName._BookName);
                        break;
                    case "bookPrice":
                        query = sortingParameters[0].Value == "DESC" ? query.OrderByDescending(a => a.BookPrice._BookPrice) : query.OrderBy(a => a.BookPrice._BookPrice);
                        break;
                }
            } else {
                //Sorting by default
                query = query.OrderBy(b => b.Id);
            }

            response.TotalRecords = query.Count();
            var numberOfItemsToSkip = pageSize * (currentPage - 1);

            query = query.Skip(numberOfItemsToSkip);
            query = query.Take(pageSize);

            var list =  await query.ToListAsync();

            response.Items = list;
            response.CurrentPage = currentPage;
            response.PageSize = pageSize;
            response.Success = true;
            response.Message = null;


            } catch (Exception ex){
                Console.WriteLine(ex);
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