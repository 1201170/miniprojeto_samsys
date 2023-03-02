using System;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Books;

public class BookPrice
{

    public double _BookPrice {get; set;}

    protected BookPrice (){
        
    }

    public BookPrice (double price) {
        if(price > 0.0){
            this._BookPrice = price;
        } else {
            throw new BusinessRuleValidationException("Error in book price","Trying to destroy the economy with a negative price but failed.");
        }
    }

}