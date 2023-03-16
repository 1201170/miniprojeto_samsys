using System;
using miniprojeto_samsys.Infrastructure.Shared;

namespace miniprojeto_samsys.Infrastructure.Entities.Authors;

public class AuthorName
{

    public String _AuthorName {get; set;}

    protected AuthorName (){

    }
    
    public AuthorName (String name) {
        if(name != null){
            this._AuthorName = name;
        } else {
            throw new BusinessRuleValidationException("Error in Author Name","Author Name must not be empty");
        }
    }

}