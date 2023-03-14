namespace miniprojeto_samsys.Domain.Authors;

public class AuthorDTO {

    public string authorId {get; set;}

    public string authorName {get; set;}

    public AuthorDTO (string authorId, string authorName){
        this.authorId = authorId;
        this.authorName = authorName;
    }

    public AuthorDTO (){
        
    }

}