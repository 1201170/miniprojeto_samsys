using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Infrastructure.Shared;

namespace miniprojeto_samsys.Infrastructure.Authors
{
    public class AuthorRepository : BaseRepository<Author, AuthorId>,IAuthorRepository
    {
        public AuthorRepository(DDDSample1DbContext context):base(context.Authors)
        {
           
        }

        public Task<List<Author>> GetByNameAsync(string authorName)
        {
            throw new NotImplementedException();
        }
    }
}