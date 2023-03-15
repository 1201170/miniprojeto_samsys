using System;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Collections.Generic;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Mappers;
using miniprojeto_samsys.Infrastructure;


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

        public async Task<MessagingHelper<List<BookDTO>>> GetAllAsync (){

            Console.WriteLine("Fetching all books");

            var response = new MessagingHelper<List<BookDTO>>();


            try{

                var responseRepository = await this._repo.GetAllBooksAsync();

                if (!responseRepository.Success)
                {
                    response.Success = false;
                    response.Message = "Erro ao obter a informação";
                    return response;
                }

                List<BookDTO> listDTO = responseRepository.Obj.ConvertAll<BookDTO>(book => new BookDTO{bookIsbn = book.Id.AsString(), 
                                    bookAuthor = book.BookAuthorID.AsString(), bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice });

                response.Obj = listDTO;
                response.Success = true;

            } catch (Exception ex){

                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }


        public async Task<PaginatedList<BookDisplayDTO>> GetBooksAsync (BookParameters bookParameters){

            var response = new PaginatedList<BookDisplayDTO>();

            Console.WriteLine("Fetching books parameters are: Page Size: " +bookParameters.PageSize+ " | Page Number: "+ bookParameters.PageNumber);

            try{

                if (bookParameters.PageSize > 20)
                {
                    bookParameters.PageSize = 20;
                }

                if (bookParameters.PageSize < 5)
                {
                    bookParameters.PageSize = 5;
                }

                if (bookParameters.PageNumber <= 0)
                {
                    bookParameters.PageNumber = 1;
                }

            var responseRepository = await this._repo.GetBooks(bookParameters);

            if (!responseRepository.Success)
            {
                    response.Success = false;
                    response.Message = "Erro ao obter a informação";
                    return response;
            }

            
            List<BookDisplayDTO> listDTO = responseRepository.Items.ConvertAll<BookDisplayDTO>(book => new BookDisplayDTO{bookIsbn = book.Id.AsString(), 
                                    bookAuthor = book.BookAuthorID.AsString(), bookAuthorName=book.Author.AuthorName._AuthorName, bookName = book.BookName._BookName, bookPrice = book.BookPrice._BookPrice });


            response.Items = listDTO;
            response.PageSize = responseRepository.PageSize;
            response.CurrentPage = responseRepository.CurrentPage;
            response.TotalRecords = responseRepository.TotalRecords;
            response.Success = true;

            } catch (Exception ex){
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }



        public async Task<MessagingHelper<BookDTO>> GetByIdAsync (BookIsbn id){

            Console.WriteLine("Fetching book with id: "+id.AsString());

            var response = new MessagingHelper<BookDTO>();


            try{

                var responseRepository = await this._repo.GetBookByIdAsync(id, includeAuthor: true);

                if (!responseRepository.Success)
                {
                    response.Success = false;
                    response.Message = "Erro ao obter a informação";
                    return response;
                }

                response.Obj = BookToBookDTOMapper.ToBookDTOMap(responseRepository.Obj);
                response.Success = true;

            } catch (Exception ex){

                
                response.Message = ex.Message;
                response.Success = false;

            }

            return response;
        }

        public async Task<MessagingHelper<BookDTO>> AddAsync(BookDTO dto){

            Console.WriteLine("Adding book");

            var response = new MessagingHelper<BookDTO>();

            try{

                await checkAuthorIdAsync(new AuthorId(dto.bookAuthor));

                var book = BookToBookDTOMapper.ToBookMap(dto);

                var bookAdded = await this._repo.AddAsync(book);

                if (bookAdded == null){

                    response.Success = false;
                    response.Message = "Erro ao adicionar livro";
                    return response;
                }

                response.Obj = BookToBookDTOMapper.ToBookDTOMap(bookAdded);
                response.Success = true;

                await this._unitOfWork.CommitAsync();

                return response;

            } catch (Exception ex){

                response.Message = ex.Message;
                response.Success = false;

                return response;
            }
        }

        public async Task<MessagingHelper<BookDTO>> UpdateAsync(string isbn ,BookDTO dto)
        {
            Console.WriteLine("Entrei PUT");

            var response = new MessagingHelper<BookDTO>();

            try{

                if(isbn != dto.bookIsbn){
                    response.Success = false;
                    response.Message = "ISBN does not match body request ISBN";
                    return response;
                }

                await checkAuthorIdAsync(new AuthorId(dto.bookAuthor));
                var book = await this._repo.GetByIdAsync(new BookIsbn(dto.bookIsbn));

                if (book == null){
                    response.Success = false;
                    response.Message = "Erro ao encontrar livro";
                    return response;
                }
                
                book.ChangeBookName(new BookName(dto.bookName));
                book.ChangeBookPrice(new BookPrice(dto.bookPrice));

                response.Obj = BookToBookDTOMapper.ToBookDTOMap(book);
                response.Success = true;

                await this._unitOfWork.CommitAsync();


            } catch (Exception ex){
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }


        public async Task<MessagingHelper<BookDTO>> DeleteAsync(BookIsbn id){

            Console.WriteLine("Deleting book with id: "+id.AsString());

            var response = new MessagingHelper<BookDTO>();

            try{

            var book = await this._repo.GetByIdAsync(id); 

            if (book == null){
                response.Success = false;
                response.Message = "Erro ao encontrar livro";
                return response;
            }

            response.Obj = BookToBookDTOMapper.ToBookDTOMap(book);
            response.Success = true;

            this._repo.Remove(book);

            await this._unitOfWork.CommitAsync();


            } catch (Exception ex){

                response.Message = ex.Message;
                response.Success = false;
            }


            return response;

        }


        public async Task<MessagingHelper<BookDTO>> SoftDeleteAsync(BookIsbn id){

            Console.WriteLine("Soft Deleting book with id: "+id.AsString());

            var response = new MessagingHelper<BookDTO>();

            try{

            var book = await this._repo.GetByIdAsync(id); 

            if (book == null){
                response.Success = false;
                response.Message = "Erro ao encontrar livro";
                return response;
            }

            response.Obj = BookToBookDTOMapper.ToBookDTOMap(book);
            response.Success = true;

            this._repo.SoftDeleteBook(book);

            await this._unitOfWork.CommitAsync();


            } catch (Exception ex){
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        private async Task checkAuthorIdAsync(AuthorId authorId)
        {
           var author = await _repoAuthor.GetByIdAsync(authorId);
           if (author == null)
                throw new BusinessRuleValidationException("Invalid Author Id.");
        }

    }
}