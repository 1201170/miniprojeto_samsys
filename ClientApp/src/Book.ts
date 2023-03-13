import BookInterface from "./BookInterface";

export class Book implements BookInterface{

    bookIsbn!: string;
    bookAuthor!: string;
    bookAuthorName!: string;
    bookName!: string;
    bookPrice!: string;

    constructor(Isbn: string, Author: string, AuthorName: string, Name: string, Price: string){
        this.bookIsbn = Isbn;
        this.bookAuthor = Author;
        this.bookAuthorName = AuthorName;
        this.bookName = Name;
        this.bookPrice = Price;
    }


}