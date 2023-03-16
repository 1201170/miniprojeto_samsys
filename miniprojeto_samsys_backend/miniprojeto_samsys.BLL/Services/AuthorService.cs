using System;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Collections.Generic;
using miniprojeto_samsys.Infrastructure.Interfaces.Repositories;
using miniprojeto_samsys.Infrastructure.Helpers;
using miniprojeto_samsys.Infrastructure.Models.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Interfaces.Services;
using miniprojeto_samsys.DAL.Repositories.Shared;
using AutoMapper;

namespace miniprojeto_samsys.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _repo;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IAuthorRepository repo, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
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

                List<AuthorDTO> listDTO = responseRepository.Obj.ConvertAll<AuthorDTO>(author => this._mapper.Map<Author,AuthorDTO>(author));

                response.Obj = listDTO;
                response.Success = true;
                return response;


            } catch (Exception ex){

                response.Message = errorMessage + ": " + ex;
                response.Success = false;
                return response;

            }

        }

        public async Task<AuthorDTO> GetByIdAsync (AuthorId id){

            Console.WriteLine("Fetching author with id: "+id);

            var author = await this._repo.GetByIdAsync(id);

            if (author == null)
                return null;

            return this._mapper.Map<Author,AuthorDTO>(author);
        }

        public async Task<AuthorDTO> AddAsync(CreatingAuthorDTO dto){

            Console.WriteLine("Adding author");
            
            var author = new Author(dto.authorName);

            await this._repo.AddAsync(author);

            await this._unitOfWork.CommitAsync();

            return this._mapper.Map<Author,AuthorDTO>(author);
        }

        public async Task<AuthorDTO> DeleteAsync(string id){

            Console.WriteLine("Deleting author with id: "+id);

            var author = await this._repo.GetByIdAsync(new AuthorId(id)); 

            if (author == null)
                return null;

            this._repo.Remove(author);
            await this._unitOfWork.CommitAsync();

            return this._mapper.Map<Author,AuthorDTO>(author);

        }

    }
}