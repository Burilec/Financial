namespace Burile.Financial.Infrastructure;

public sealed class PaginatedResult<T>(int pageNumber, int pageSize, int totalRecords, IEnumerable<T> records)
    where T : class
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalPages => Convert.ToInt32(Math.Ceiling((double)TotalRecords / PageSize));
    public int TotalRecords { get; } = totalRecords;
    public IEnumerable<T> Records { get; } = records;
}