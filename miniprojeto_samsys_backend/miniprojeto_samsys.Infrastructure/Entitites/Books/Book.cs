using System;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Interfaces.Repositories;
using miniprojeto_samsys.Infrastructure.Shared;


namespace miniprojeto_samsys.Infrastructure.Entities.Books;

public class Book : Entity<BookIsbn>, IAggregateRoot{

    public BookName BookName {get; set;}
    public BookPrice BookPrice {get; set;}
    public AuthorId BookAuthorID {get; set;}
    public Boolean isActive {get; set;}
    public Author Author {get; set;}

    public Book(){

    }

    public Book(string isbn, String name, string price, AuthorId authorId){
        this.Id = new BookIsbn(isbn);
        if (authorId.ToString() == null){
            throw new BusinessRuleValidationException("Error in book author","Every book must have an author");
        }
        this.BookAuthorID = authorId;
        this.BookName = new BookName(name);
        this.BookPrice = new BookPrice(price);
        this.isActive = true;
    }
    
    public void ChangeBookName(BookName bookName)
    {
            
            if (!this.isActive)
                throw new BusinessRuleValidationException("It is not possible to change the name of an inactive product.");
            
            this.BookName = bookName;
    }

        public void ChangeBookPrice(BookPrice bookPrice)
    {
            
            if (!this.isActive)
                throw new BusinessRuleValidationException("It is not possible to change the price of an inactive product.");
            
            this.BookPrice = bookPrice;
    }

}