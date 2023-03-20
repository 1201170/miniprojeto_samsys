using System;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Collections.Generic;
using miniprojeto_samsys.Infrastructure.Interfaces.Services;
using miniprojeto_samsys.Infrastructure.Interfaces.Repositories;
using miniprojeto_samsys.Infrastructure.Helpers;
using miniprojeto_samsys.Infrastructure.Models.Books;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.DAL.Repositories.Shared;
using miniprojeto_samsys.BLL.Mappers;
using AutoMapper;
using miniprojeto_samsys.Infrastructure.Models.Search;

namespace miniprojeto_samsys.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _repo;
        private readonly IAuthorRepository _repoAuthor;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IBookRepository repo, IAuthorRepository repoAuthor, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoAuthor = repoAuthor;
            this._mapper = mapper;
        }

        public BookService()
        {
            //ensure utility
        }

        public async Task<MessagingHelper<List<BookDTO>>> GetAllAsync (){

            Console.WriteLine("Fetching all books");

            var response = new MessagingHelper<List<BookDTO>>();
            string errorMessage = "Error occurred while obtaining data";


            try{

                var responseRepository = await this._repo.GetAllBooksAsync();

                if (!responseRepository.Success)
                {
                    response.Success = false;
                    response.Message = errorMessage;
                    return response;
                }

                List<BookDTO> listDTO = responseRepository.Obj.ConvertAll<BookDTO>(book => this._mapper.Map<Book, BookDTO>(book));

                response.Obj = listDTO;
                response.Success = true;
                return response;


            } catch (Exception ex){

                response.Message = errorMessage + ": " + ex;
                response.Success = false;
                return response;

            }

        }


        public async Task<PaginatedList<BookDisplayDTO>> GetBooksAsync (SearchDTO search){

            var response = new PaginatedList<BookDisplayDTO>();
            string errorMessage = "Error occurred while obtaining data";


            Console.WriteLine("Fetching books parameters are: Page Size: " +search.PageSize+ " | Page Number: "+ search.CurrentPage);

            try{

            search.Validate();

            var responseRepository = await this._repo.GetBooks(search.SearchParameters, search.SortingParameters, search.CurrentPage, search.PageSize);

            if (!responseRepository.Success)
            {
                    response.Success = false;
                    response.Message = errorMessage;
                    return response;
            }
            
            List<BookDisplayDTO> listDTO = responseRepository.Items.ConvertAll<BookDisplayDTO>(book => this._mapper.Map<Book, BookDisplayDTO>(book));


            response.Items = listDTO;
            response.PageSize = responseRepository.PageSize;
            response.CurrentPage = responseRepository.CurrentPage;
            response.TotalRecords = responseRepository.TotalRecords;
            response.Success = true;
            return response;

            } catch (Exception ex){
                response.Message = errorMessage + ": " + ex;
                response.Success = false;
                return response;

            }
        }



        public async Task<MessagingHelper<BookDTO>> GetByIdAsync (BookIsbn id){

            Console.WriteLine("Fetching book with id: "+id.AsString());

            var response = new MessagingHelper<BookDTO>();

            string errorMessage = "Error occurred while obtaining data";


            try{

                var responseRepository = await this._repo.GetBookByIdAsync(id, includeAuthor: true);

                if (!responseRepository.Success)
                {
                    response.Success = false;
                    response.Message = errorMessage;
                    return response;
                }

                response.Obj = this._mapper.Map<Book,BookDTO>(responseRepository.Obj);
                response.Success = true;
                return response;


            } catch (Exception ex){
                
                response.Message = errorMessage + ": " + ex;
                response.Success = false;
                return response;


            }

        }

        public async Task<MessagingHelper<BookDTO>> AddAsync(BookDTO dto){

            Console.WriteLine("Adding book");

            var response = new MessagingHelper<BookDTO>();

            string errorMessage = "Error occured while adding book";
            string errorMessage2 = "Book with specified ISBN already exists";
            string errorMessage3 = "Could not find an author with specified ID";



            try{

                var bookExists = await this._repo.GetByIdAsync(new BookIsbn(dto.bookIsbn));

                if (bookExists!=null){
                    response.Success = false;
                    response.Message = errorMessage2;
                    return response;
                }

                
                var author = await checkAuthorIdAsync(new AuthorId(dto.bookAuthor));

                if(author == null){
                    response.Success = false;
                    response.Message = errorMessage3;
                    return response;
                }

                var book = this._mapper.Map<BookDTO, Book>(dto);
                book.isActive = true;

                Console.WriteLine(book.Id.AsString());
                Console.WriteLine(book.BookName._BookName);
                Console.WriteLine(book.BookPrice._BookPrice);
                Console.WriteLine(book.BookAuthorID.AsString());


                var bookAdded = await this._repo.AddAsync(book);

                if (bookAdded == null){

                    response.Success = false;
                    response.Message = errorMessage;
                    return response;
                }

                response.Obj = this._mapper.Map<Book,BookDTO>(bookAdded);
                response.Success = true;

                await this._unitOfWork.CommitAsync();

                return response;

            } catch (Exception ex){

                response.Message = errorMessage + ": " + ex;
                response.Success = false;

                return response;
            }
        }

        public async Task<MessagingHelper<BookDTO>> UpdateAsync(string isbn ,BookDTO dto)
        {
            Console.WriteLine("Entrei PUT");

            var response = new MessagingHelper<BookDTO>();

            string errorMessage = "ISBN does not match body request ISBN";
            string errorMessage2 = "Error occured while changing book data";
            string errorMessage3 = "Error occured while finding the book ISBN";
            string errorMessage4 = "Could not find an author with specified ID";



            try{

                if(isbn != dto.bookIsbn){
                    response.Success = false;
                    response.Message = errorMessage;
                    return response;
                }

                var author = await checkAuthorIdAsync(new AuthorId(dto.bookAuthor));

                if(author == null){
                    response.Success = false;
                    response.Message = errorMessage4;
                    return response;
                }

                var book = await this._repo.GetByIdAsync(new BookIsbn(dto.bookIsbn));

                if (book == null){
                    response.Success = false;
                    response.Message = errorMessage3;
                    return response;
                }
                
                book.ChangeBookName(new BookName(dto.bookName));
                book.ChangeBookPrice(new BookPrice(dto.bookPrice));

                response.Obj = this._mapper.Map<Book,BookDTO>(book);
                response.Success = true;

                await this._unitOfWork.CommitAsync();
                return response;



            } catch (Exception ex){
                response.Message = errorMessage2 + ": " + ex;
                response.Success = false;
                return response;

            }

        }


        public async Task<MessagingHelper<BookDTO>> DeleteAsync(BookIsbn id){

            Console.WriteLine("Deleting book with id: "+id.AsString());

            string errorMessage = "Error occured while finding the book ISBN";
            string errorMessage2 = "Error occured while trying to delete book";

            var response = new MessagingHelper<BookDTO>();

            try{

            var book = await this._repo.GetByIdAsync(id); 

            if (book == null){
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            response.Obj = this._mapper.Map<Book,BookDTO>(book);
            response.Success = true;

            this._repo.Remove(book);

            await this._unitOfWork.CommitAsync();
            return response;



            } catch (Exception ex){

                response.Message = errorMessage2 + ": " + ex;
                response.Success = false;
                return response;

            }


        }


        public async Task<MessagingHelper<BookDTO>> SoftDeleteAsync(BookIsbn id){

            Console.WriteLine("Soft Deleting book with id: "+id.AsString());

            string errorMessage = "Error occured while finding the book ISBN";
            string errorMessage2 = "Error occured while trying to delete book";            

            var response = new MessagingHelper<BookDTO>();

            try{

            var book = await this._repo.GetByIdAsync(id); 

            if (book == null){
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }

            response.Obj = this._mapper.Map<Book,BookDTO>(book);
            response.Success = true;

            this._repo.SoftDeleteBook(book);

            await this._unitOfWork.CommitAsync();
            return response;



            } catch (Exception ex){
                response.Message = errorMessage2 + ": " + ex;
                response.Success = false;
                return response;

            }

        }

        private async Task<Author> checkAuthorIdAsync(AuthorId authorId)
        {
           var author = await _repoAuthor.GetByIdAsync(authorId);
           return author;
        }

    }
}