using System;
using miniprojeto_samsys.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using miniprojeto_samsys.Infrastructure;

namespace miniprojeto_samsys.Domain.Authors
{
    public interface IAuthorRepository:IRepository<Author,AuthorId>
    {
        Task<List<Author>> GetByNameAsync(String authorName);

        Task<MessagingHelper<List<Author>>> GetAllAuthorsAsync();
        
    }
}