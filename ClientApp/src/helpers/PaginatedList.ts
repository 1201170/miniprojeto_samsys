export class PaginatedList<T> {
    success: boolean;
    message: string
    name: string;
    items: T[];
    totalPages: number;
    count: number = 0;
    totalRecords: number = 0;
    pageSize: number = 0;

    constructor(success: boolean, message: string, name: string, items: T[], totalPages: number) {
        this.success = success;
        this.message = message;
        this.name = name;
        this.items = items;
        this.totalPages = totalPages;
    }
}