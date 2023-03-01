using System;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Authors;

public class AuthorName
{

    public String _AuthorName {get; set;}
    public AuthorName (String name) {
        if(name != null){
            this._AuthorName = name;
        } else {
            throw new Exception("Author Name must not be empty");
        }
    }

}