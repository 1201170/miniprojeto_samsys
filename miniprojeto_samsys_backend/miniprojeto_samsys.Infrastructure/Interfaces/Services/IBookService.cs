
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.Infrastructure.Helpers;
using miniprojeto_samsys.Infrastructure.Models.Books;
using miniprojeto_samsys.Infrastructure.Models.Search;

namespace miniprojeto_samsys.Infrastructure.Interfaces.Services{

    public interface IBookService{

        Task<MessagingHelper<List<BookDTO>>> GetAllAsync ();

        Task<PaginatedList<BookDisplayDTO>> GetBooksAsync (SearchDTO search);

        Task<MessagingHelper<BookDTO>> GetByIdAsync (BookIsbn id);

        Task<MessagingHelper<BookDTO>> AddAsync(BookDTO dto);

        Task<MessagingHelper<BookDTO>> UpdateAsync(string isbn ,BookDTO dto);

        Task<MessagingHelper<BookDTO>> DeleteAsync(BookIsbn id);

        Task<MessagingHelper<BookDTO>> SoftDeleteAsync(BookIsbn id);

    }
}
