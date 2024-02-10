using FluentResults;
using MediatR;

namespace Burile.Financial.Api.Features.RetrieveProductByApiId;

public sealed class RetrieveProductByApiIdQuery(Guid apiId)
    : IRequest<Result<RetrieveProductByApiIdResponse>>
{
    public Guid ApiId { get; } = apiId;
}