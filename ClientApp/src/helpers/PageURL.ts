class PageURLClass {

    public GetSearchParameters(): string {
        return (window.location.search);
    }
}

export const PageURL = new PageURLClass();