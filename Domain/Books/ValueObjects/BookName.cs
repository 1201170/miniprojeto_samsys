using System;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Books;

public class BookName
{

    public String _BookName {get; set;}

    protected BookName (){
        
    }

    public BookName (String name) {
        if(name != null){
            this._BookName = name;
        } else {
            throw new BusinessRuleValidationException("Error in Book Name","Book Name must not be empty");
        }
    }

}