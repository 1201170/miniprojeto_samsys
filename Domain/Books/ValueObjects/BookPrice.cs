using System;
using miniprojeto_samsys.Domain.Shared;

namespace miniprojeto_samsys.Domain.Books;

public class BookPrice
{

    public double _BookPrice {get; set;}
    public BookPrice (double price) {
        if(price > 0.0){
            this._BookPrice = price;
        } else {
            throw new Exception("Trying to destroy the economy but failed.");
        }
    }

}