using AutoMapper;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Books;
using miniprojeto_samsys.Infrastructure.Models.Authors;
using miniprojeto_samsys.Infrastructure.Models.Books;

namespace miniprojeto_samsys.BLL.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDTO>().
            ForMember(dest => dest.bookIsbn, opt => opt.MapFrom(src => src.Id.AsString())).
            ForMember(dest => dest.bookAuthor, opt => opt.MapFrom(src => src.BookAuthorID.AsString())).
            ForMember(dest => dest.bookName, opt => opt.MapFrom(src => src.BookName._BookName)).
            ForMember(dest => dest.bookPrice, opt => opt.MapFrom(src => src.BookPrice._BookPrice));

            CreateMap<BookDTO, Book>().
            ForMember(dest => dest.Id, opt => opt.MapFrom(src => new BookIsbn(src.bookIsbn))).
            ForMember(dest => dest.BookName, opt => opt.MapFrom(src => new BookName(src.bookName))).
            ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => new BookPrice(src.bookPrice))).
            ForMember(dest => dest.BookAuthorID, opt => opt.MapFrom(src => new AuthorId(src.bookAuthor)));
            CreateMap<Book, BookDisplayDTO>().
            ForMember(dest => dest.bookIsbn, opt => opt.MapFrom(src => src.Id.AsString())).
            ForMember(dest => dest.bookAuthor, opt => opt.MapFrom(src => src.BookAuthorID.AsString())).
            ForMember(dest => dest.bookAuthorName, opt => opt.MapFrom(src => src.Author.AuthorName._AuthorName)).
            ForMember(dest => dest.bookName, opt => opt.MapFrom(src => src.BookName._BookName)).
            ForMember(dest => dest.bookPrice, opt => opt.MapFrom(src => src.BookPrice._BookPrice));

            CreateMap<Author, AuthorDTO>().
            ForMember(dest => dest.authorId, opt => opt.MapFrom(src => src.Id.AsString())).
            ForMember(dest => dest.authorName, opt => opt.MapFrom(src => src.AuthorName._AuthorName));

        }
    }
}