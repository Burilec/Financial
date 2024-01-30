namespace Burile.Financial.Infrastructure;

public sealed record PagingOptions(int PageNumber = 1, int PageSize = 10);