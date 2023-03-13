namespace miniprojeto_samsys.Domain.Books;

public class BookDisplayDTO {

    public string bookIsbn {get; set;}

    public string bookAuthor {get; set;}

    public string bookAuthorName{get; set;}

    public string bookName {get; set;}

    public string bookPrice {get; set;}


    public BookDisplayDTO (string bookIsbn, string bookAuthorID, string bookAuthorName, string bookName, string bookPrice){
        this.bookIsbn = bookIsbn;
        this.bookAuthor = bookAuthorID;
        this.bookAuthorName = bookAuthorName;
        this.bookName = bookName;
        this.bookPrice = bookPrice;
    }

    public BookDisplayDTO (){
        
    }

}