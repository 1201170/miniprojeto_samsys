using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Infrastructure.Shared;

namespace miniprojeto_samsys.Infrastructure.Authors
{
    public class AuthorRepository : BaseRepository<Author, AuthorId>,IAuthorRepository
    {

        private readonly DbSet<Author> _objs;

        public AuthorRepository(DDDSample1DbContext context):base(context.Authors)
        {
           this._objs = context.Authors;
        }

        public Task<List<Author>> GetByNameAsync(string authorName)
        {
            throw new NotImplementedException();
        }

        public async Task<MessagingHelper<List<Author>>> GetAllAuthorsAsync()
        {
            var response = new MessagingHelper<List<Author>>();

            try{

                var authorList = await this._objs.ToListAsync();

                response.Obj = authorList;
                response.Success = true;
                response.Message = null;

            } catch (Exception ex) {

                response.Success = false;
                response.Message = ex.Message;

            }

            return response;
        }

    }
}