using FluentResults;

namespace Burile.Financial.Api.Features.Products.RetrieveProductByApiId;

public sealed class RetrieveProductByApiIdQuery(Guid apiId)
    : IRequest<Result<RetrieveProductByApiIdResponse>>
{
    public Guid ApiId { get; } = apiId;
}