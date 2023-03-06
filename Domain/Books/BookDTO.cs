namespace miniprojeto_samsys.Domain.Books;

public class BookDTO {

    public string bookIsbn {get; set;}

    public string bookAuthor {get; set;}

    public string bookName {get; set;}

    public double bookPrice {get; set;}


    public BookDTO (string bookIsbn, string bookAuthorID, string bookName, double bookPrice){
        this.bookIsbn = bookIsbn;
        this.bookAuthor = bookAuthorID;
        this.bookName = bookName;
        this.bookPrice = bookPrice;
    }

    public BookDTO (){
        
    }

}