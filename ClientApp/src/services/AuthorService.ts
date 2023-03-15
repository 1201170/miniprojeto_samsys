import { MessagingHelper } from "../helpers/MessagingHelper";
import Author from "../models/Author/Author";
import { APIService } from "./APIService";

export class AuthorService{

    async GetAll(): Promise<MessagingHelper<Author[]>> {
        try {
            var response = await APIService.Axios().get(`${APIService.GetURL()}/author`, 
            {}
            );
            return response.data;
        } catch (error) {
            return new MessagingHelper<Author[]>(false, "Erro ao obter a informação dos autores",[]);
        };
    } 

}