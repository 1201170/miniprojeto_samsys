namespace miniprojeto_samsys.Domain.Books;

public class BookDTO {

    public int bookIsbn {get; set;}

    public string bookAuthor {get; set;}

    public string bookName {get; set;}

    public double bookPrice {get; set;}


    public BookDTO (int bookIsbn, string bookAuthor, string bookName, double bookPrice){
        this.bookIsbn = bookIsbn;
        this.bookAuthor = bookAuthor;
        this.bookName = bookName;
        this.bookPrice = bookPrice;
    }

    public BookDTO (){
        
    }

}