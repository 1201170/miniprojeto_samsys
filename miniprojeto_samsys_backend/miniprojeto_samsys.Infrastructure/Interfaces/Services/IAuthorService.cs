
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Helpers;
using miniprojeto_samsys.Infrastructure.Models.Authors;

namespace miniprojeto_samsys.Infrastructure.Interfaces.Services{

    public interface IAuthorService{

        Task<MessagingHelper<List<AuthorDTO>>> GetAllAsync ();

        Task<AuthorDTO> GetByIdAsync (AuthorId id);

        Task<AuthorDTO> AddAsync(CreatingAuthorDTO dto);

        Task<AuthorDTO> DeleteAsync(string id);

    }
}
