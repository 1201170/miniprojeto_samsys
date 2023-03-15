export class MessagingHelper<T> {
    success: boolean;
    message: string;
    obj: T;
    errorType: ErrorType | null = null;

    constructor(success: boolean, message: string, obj: T) {
        this.success = success;
        this.message = message;
        this.obj = obj;
    }
}

export enum ErrorType {
    DataHasChanged = "DataHasChanged"
}