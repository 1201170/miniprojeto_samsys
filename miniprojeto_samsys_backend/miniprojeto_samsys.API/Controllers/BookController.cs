using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using miniprojeto_samsys.BLL.Services;
using miniprojeto_samsys.Infrastructure.Helpers;
using miniprojeto_samsys.Infrastructure.Models.Books;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.Infrastructure.Models.Search;

namespace miniprojeto_samsys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<MessagingHelper<List<BookDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Books/id
        [HttpGet("{id}")]
        public async Task<MessagingHelper<BookDTO>> GetById(string id)
        {
            return await _service.GetByIdAsync(new BookIsbn(id));
        }

        [HttpPost("GetAll")]
        public async Task<PaginatedList<BookDisplayDTO>> GetBooks(SearchDTO search)
        {
            var books = await _service.GetBooksAsync(search);

            Console.WriteLine("Rows fetched: "+books.TotalRecords);

            return books;
        }

        
        [HttpPost]
        public async Task<MessagingHelper<BookDTO>> Create(BookDTO dto)
        {

                return await _service.AddAsync(dto);
        }

        [HttpPut("{isbn}")]
        public async Task<MessagingHelper<BookDTO>> Update(string isbn, BookDTO dto){

                return await _service.UpdateAsync(isbn,dto);
        }


        [HttpDelete("{id}/hardDelete")]
        public async Task<MessagingHelper<BookDTO>> HardDelete(string id)
        {
             return await _service.DeleteAsync(new BookIsbn(id));
        }

        [HttpDelete("{id}/softDelete")]
        public async Task<MessagingHelper<BookDTO>> SoftDelete(string id)
        {
            return await _service.SoftDeleteAsync(new BookIsbn(id));
        }
    }
}