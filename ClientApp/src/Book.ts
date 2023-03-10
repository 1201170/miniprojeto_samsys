import BookInterface from "./BookInterface";

export class Book implements BookInterface{

    bookIsbn!: string;
    bookAuthor!: string;
    bookName!: string;
    bookPrice!: string;

    constructor(Isbn: string, Author: string, Name: string, Price: string){
        this.bookIsbn = Isbn;
        this.bookAuthor = Author;
        this.bookName = Name;
        this.bookPrice = Price;
    }


}