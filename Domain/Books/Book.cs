using System;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Books;

public class Book : Entity<BookIsbn>, IAggregateRoot{

    public BookName BookName {get; set;}
    public BookPrice BookPrice {get; set;}

    protected Book (){
        
    }

    public Book(int isbn, String name, double price){
        this.Id = new BookIsbn(isbn);
        this.BookName = new BookName(name);
        this.BookPrice = new BookPrice(price);
    }

}