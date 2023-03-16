namespace miniprojeto_samsys.Infrastructure.Models.Authors;

public class CreatingAuthorDTO {

    public string authorName {get; set;}

    public CreatingAuthorDTO (string authorName){
        this.authorName = authorName;
    }

    public CreatingAuthorDTO (){
        
    }

}