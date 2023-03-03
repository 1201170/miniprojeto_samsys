import BookInterface from "./BookInterface";

export class Book implements BookInterface{

    bookIsbn!: string;
    bookAuthor!: string;
    bookName!: string;
    bookPrice!: number;

    public Book(Isbn: string, Author: string, Name: string, Price: number){
        this.bookIsbn = Isbn;
        this.bookAuthor = Author;
        this.bookName = Name;
        this.bookPrice = Price;
    }


}