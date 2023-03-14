using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Domain.Books;
using Newtonsoft.Json;

namespace miniprojeto_samsys.Controllers
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
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Books/id
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(string id)
        {
            var book = await _service.GetByIdAsync(new BookIsbn(id));

            if (book == null){
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet("pageNumber={pageNumber}&pageSize={pageSize}")]
        public async Task<ActionResult<BookDTO>> GetBooks(int pageNumber, int pageSize)
        {
            BookParameters bookParameters = new BookParameters(pageNumber, pageSize);

            var books = await _service.GetBooksAsync(bookParameters);

            var metadata = new
            {
                books.TotalCount,
                books.PageSize,
                books.CurrentPage,
                books.TotalPages,
                books.HasNext,
                books.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            Console.WriteLine("Rows fetched: "+books.TotalCount);

            return Ok(books);
        }

        
        [HttpPost]
        public async Task<ActionResult<BookDTO>> Create(BookDTO dto)
        {
            try
            {
                var book = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = book.bookIsbn }, book);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpPut("{isbn}")]
        public async Task<ActionResult<BookDTO>> Update(string isbn, BookDTO dto){
            if (isbn != dto.bookIsbn)
            {
                return BadRequest();
            }

            try
            {
                var book = await _service.UpdateAsync(dto);
                
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }


        [HttpDelete("{id}/hardDelete")]
        public async Task<ActionResult<BookDTO>> HardDelete(string id)
        {
            try{
                var book = await _service.DeleteAsync(new BookIsbn(id));

                if (book == null){
                    return NotFound();
                }

                return Ok(book);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpDelete("{id}/softDelete")]
        public async Task<ActionResult<BookDTO>> SoftDelete(string id)
        {
            try{
                var book = await _service.SoftDeleteAsync(new BookIsbn(id));

                if (book == null){
                    return NotFound();
                }

                return Ok(book);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }


    }
}