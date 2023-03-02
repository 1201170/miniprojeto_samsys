using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Domain.Books;

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
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            var book = await _service.GetByIdAsync(new BookIsbn(id));

            if (book == null){
                return NotFound();
            }

            return book;
        }
        
    }
}