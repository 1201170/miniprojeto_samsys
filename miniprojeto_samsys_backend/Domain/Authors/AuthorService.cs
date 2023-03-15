using System;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Collections.Generic;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Infrastructure;

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

        public async Task<MessagingHelper<List<AuthorDTO>>> GetAllAsync (){

            Console.WriteLine("Fetching all authors");

            var response = new MessagingHelper<List<AuthorDTO>>();

            string errorMessage = "Error occured while obtain authors data";

            try{

                var responseRepository = await this._repo.GetAllAuthorsAsync();

                if (!responseRepository.Success)
                {
                    response.Success = false;
                    response.Message = errorMessage;
                    return response;
                }

                List<AuthorDTO> listDTO = responseRepository.Obj.ConvertAll<AuthorDTO>(author => new AuthorDTO{authorId = author.Id.AsString(), 
                                    authorName = author.AuthorName._AuthorName});

                response.Obj = listDTO;
                response.Success = true;
                return response;


            } catch (Exception ex){

                response.Message = errorMessage;
                response.Success = false;
                return response;

            }

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