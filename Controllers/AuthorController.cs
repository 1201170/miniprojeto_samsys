using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Domain.Authors;

namespace miniprojeto_samsys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorController(AuthorService service)
        {
            _service = service;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Author/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetById(string id)
        {
            var author = await _service.GetByIdAsync(new AuthorId(id));

            if (author == null){
                return NotFound();
            }

            return author;
        }
        
        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Create(CreatingAuthorDTO dto)
        {
            try
            {
                var author = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = author.authorId }, author);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }


        [HttpDelete("{id}/hardDelete")]
        public async Task<ActionResult<AuthorDTO>> HardDelete(string id)
        {
            try{
                var author = await _service.DeleteAsync(id);

                if (author == null){
                    return NotFound();
                }

                return Ok(author);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }

    }
}