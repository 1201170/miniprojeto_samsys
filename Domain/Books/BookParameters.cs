
namespace miniprojeto_samsys.Domain.Books;

public class BookParameters
{
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }

    public BookParameters(int pageNumber, int pageSize){
        this.PageNumber = pageNumber;
        this._pageSize = pageSize;
    }
}