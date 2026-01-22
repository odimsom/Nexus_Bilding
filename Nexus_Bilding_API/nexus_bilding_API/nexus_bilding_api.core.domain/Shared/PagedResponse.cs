namespace nexus_bilding_api.core.domain.Shared;

public class PagedResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
    where T : class
{
    public IEnumerable<T> Data { get; set; } = data;
    private int PageNumber { get; set; } = pageNumber;
    private int PageSize { get; set; } = pageSize;
    private int TotalRecords { get; set; } = totalRecords;
    public bool HasNextPage => PageNumber * PageSize < TotalRecords;
    public bool HasPreviousPage => PageNumber > 1;
}