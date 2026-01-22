namespace nexus_bilding_api.core.domain.Wrappers;

public class PaginatedResponse<TData> where TData : class
{
    public PaginatedResponse<TMap> Map<TMap>(Func<TData, TMap> mapFunc) where TMap : class
    {
        var data = Data.Select(mapFunc);
        return new PaginatedResponse<TMap>
        {
            Data = data,
            Pagination = this.Pagination
        };
    }
    
    public required IEnumerable<TData> Data { get; init; }
    public required Pagination Pagination { get; init; }
}