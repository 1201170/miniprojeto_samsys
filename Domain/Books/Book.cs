using System;
using miniprojeto_samsys.Domain.Authors;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Books;

public class Book : Entity<BookIsbn>, IAggregateRoot{

    public BookName BookName {get; set;}
    public BookPrice BookPrice {get; set;}
    public AuthorId BookAuthorID {get; set;}
    public Boolean isActive {get; set;}
    //public Author Author {get; set;}

    protected Book (){
        
    }

    public Book(string isbn, String name, double price, AuthorId authorId){
        this.Id = new BookIsbn(isbn);
        if (authorId.ToString() == null){
            throw new BusinessRuleValidationException("Error in book author","Every book must have an author");
        }
        this.BookAuthorID = authorId;
        this.BookName = new BookName(name);
        this.BookPrice = new BookPrice(price);
        this.isActive = true;
    }

}