using System;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Collections.Generic;
using miniprojeto_samsys.Domain.Books;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Authors
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _repo;

        public AuthorService(IUnitOfWork unitOfWork, IAuthorRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public AuthorService()
        {
            //ensure utility
        }
    }
}