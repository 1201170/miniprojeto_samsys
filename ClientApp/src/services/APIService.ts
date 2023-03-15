import axios, { AxiosInstance } from "axios";
class APIServiceClass {

    private url: string = (process.env.NODE_ENV === 'development') ? "https://localhost:5001/api" : "/api";
    private instance = axios.create();

    constructor() {}

    GetURL(): string {
        return this.url;
    }

    Axios(): AxiosInstance {
        return this.instance;
    }

}

export const APIService = new APIServiceClass();