using System;
using miniprojeto_samsys.Domain.Shared;
using miniprojeto_samsys.Domain.Books;


namespace miniprojeto_samsys.Domain.Authors;

public class Author : Entity<AuthorId>, IAggregateRoot{

    public AuthorName AuthorName {get; set;}

    protected Author (){

    }

    public Author(String AuthorName){
        this.Id = new AuthorId(Guid.NewGuid());
        this.AuthorName = new AuthorName(AuthorName);
    }

    public ICollection<Book> Books {get; set;}

}