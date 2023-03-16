using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.DAL.Repositories.Shared;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Helpers;

namespace miniprojeto_samsys.Infrastructure.Interfaces.Repositories
{
    public interface IAuthorRepository:IRepository<Author,AuthorId>
    {
        Task<List<Author>> GetByNameAsync(String authorName);

        Task<MessagingHelper<List<Author>>> GetAllAuthorsAsync();
        
    }
}