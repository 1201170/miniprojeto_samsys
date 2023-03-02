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

        public async Task<List<AuthorDTO>> GetAllAsync (){

            Console.WriteLine("Fetching all authors");

            var list = await this._repo.GetAllAsync();

            List<AuthorDTO> listDTO = list.ConvertAll<AuthorDTO>(author => new AuthorDTO{authorId = author.Id.AsString(), 
                                    authorName = author.AuthorName._AuthorName});
            return listDTO;
        }

        public async Task<AuthorDTO> GetByIdAsync (AuthorId id){

            Console.WriteLine("Fetching author with id: "+id);

            var author = await this._repo.GetByIdAsync(id);

            if (author == null)
                return null;

            return new AuthorDTO{authorId = author.Id.AsString(), authorName = author.AuthorName._AuthorName};
        }

        public async Task<AuthorDTO> AddAsync(CreatingAuthorDTO dto){

            Console.WriteLine("Adding author");
            
            var author = new Author(dto.authorName);

            await this._repo.AddAsync(author);

            await this._unitOfWork.CommitAsync();

            return new AuthorDTO{authorId = author.Id.AsString(), authorName = author.AuthorName._AuthorName};
        }

        public async Task<AuthorDTO> DeleteAsync(string id){

            Console.WriteLine("Deleting author with id: "+id);

            var author = await this._repo.GetByIdAsync(new AuthorId(id)); 

            if (author == null)
                return null;

            this._repo.Remove(author);
            await this._unitOfWork.CommitAsync();

            return new AuthorDTO{authorId = author.Id.AsString(), authorName = author.AuthorName._AuthorName};

        }

    }
}