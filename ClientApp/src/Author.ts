import AuthorInterface from "./AuthorInterface";

export class Author implements AuthorInterface{

    authorId: string;
    authorName: string;

    constructor(Id: string, Name: string){
        this.authorId = Id;
        this.authorName = Name;
    }

}