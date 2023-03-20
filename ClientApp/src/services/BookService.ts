import { Id } from "react-toastify";
import { MessagingHelper } from "../helpers/MessagingHelper";
import { PaginatedList } from "../helpers/PaginatedList";
import Book from "../models/Book/Book";
import BookCreationDTO from "../models/Book/BookCreationDTO";
import { Parameter } from "../models/Parameter";
import { APIService } from "./APIService";

export class BookService{


    async GetBooks(
        searchParameters: Parameter[],
        sortingParameters: Parameter[],
        currentPage: number,
        pageSize: number
    ): Promise<PaginatedList<Book>> {
        try{
            var response = await APIService.Axios().post(
                `${APIService.GetURL()}/book/GetAll`,
                {
                        currentPage,
                        pageSize,
                        searchParameters,
                        sortingParameters
                }
            );
            return response.data;
        }catch(error) {
            return new PaginatedList<Book>(
                false,
                "Error obtaining book information",
                "",
                [],
                0,
            );
        }
    }



    async CreateBook(createBookStatus: BookCreationDTO): Promise<MessagingHelper<Book | null>> {
        try {
            var response = await APIService.Axios().post(`${APIService.GetURL()}/book`,
                {
                    ...createBookStatus
                },
                {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    }
                });
            return response.data;
        } catch (error) {
            return new MessagingHelper<Book | null>(false, "Erro ao criar o livro", null);
        };
    }

    async Edit(bookUpdate: BookCreationDTO): Promise<MessagingHelper<BookCreationDTO | null>> {
        try {
            var response = await APIService.Axios().put(
                `${APIService.GetURL()}/book/${bookUpdate.bookIsbn}`,
                { ...bookUpdate },
                {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    },
                },
            );
            return response.data;
        } catch (error) {
            return new MessagingHelper<BookCreationDTO | null>(
                false,
                "Erro ao ligar ao servidor para atualizar o livro",
                null,
            );
        }
    }


    async Delete(isbn: string): Promise<MessagingHelper<Book | null>> {
        try {
            var response = await APIService.Axios().delete(
                `${APIService.GetURL()}/book/${isbn}/softDelete`,   {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    },
                },
            );
            return response.data;
        } catch (error) {
            return new MessagingHelper(
                false,
                "Erro ao ligar ao servidor para eliminar o livro",
                null,
            );
        }
    }
}