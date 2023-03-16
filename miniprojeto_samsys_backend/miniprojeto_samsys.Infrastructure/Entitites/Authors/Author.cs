using System;
using System.Collections.Generic;
using miniprojeto_samsys.Infrastructure.Shared;
using miniprojeto_samsys.Infrastructure.Interfaces.Repositories;
using miniprojeto_samsys.Infrastructure.Entities.Books;

namespace miniprojeto_samsys.Infrastructure.Entities.Authors;

public class Author : Entity<AuthorId>, IAggregateRoot{

    public AuthorName AuthorName {get; set;}

    public List<Book> Books { get; set; }

    protected Author (){

    }

    public Author(String AuthorName){
        this.Id = new AuthorId(Guid.NewGuid());
        this.AuthorName = new AuthorName(AuthorName);
    }
}