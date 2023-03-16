using System;
using System.Globalization;
using System.Text.RegularExpressions;
using miniprojeto_samsys.Infrastructure.Shared;

namespace miniprojeto_samsys.Infrastructure.Entities.Books;

public class BookPrice
{

    public string _BookPrice {get; set;}


    protected BookPrice (){
        
    }

    public BookPrice (string price) {
        Match match = Regex.Match(price, @"^[0-9]+(\.[0-9]{2})?$");

        if(double.Parse(price, CultureInfo.InvariantCulture) > 0.0 && match.Success){
            this._BookPrice = price;
        } else {
            throw new BusinessRuleValidationException("Error in book price","Trying to destroy the economy with a negative price but failed.");
        }
    }

}