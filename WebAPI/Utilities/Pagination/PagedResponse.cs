namespace WebAPI.Utilities.Pagination;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;


}
