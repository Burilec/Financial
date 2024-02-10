using FluentResults;
using MediatR;

namespace Burile.Financial.Api.Features.UpdateProduct;

public sealed class UpdateProductCommand(Guid apiId, UpdateProductRequest updateProductRequest) : IRequest<Result>
{
    public Guid ApiId { get; } = apiId;
    public string? Name { get; } = updateProductRequest.Name;
    public string? Currency { get; } = updateProductRequest.Currency;
    public string? Exchange { get; } = updateProductRequest.Exchange;
    public string? Country { get; } = updateProductRequest.Country;
    public string? MicCode { get; } = updateProductRequest.MicCode;
}