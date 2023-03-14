namespace miniprojeto_samsys.Domain.Authors;

public class CreatingAuthorDTO {

    public string authorName {get; set;}

    public CreatingAuthorDTO (string authorName){
        this.authorName = authorName;
    }

    public CreatingAuthorDTO (){
        
    }

}