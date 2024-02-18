using Burile.Financial.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

[ApiController]
[Route("api")]
public sealed class RetrieveProductsEndpoint : ControllerBase
{
    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              [FromQuery] PagingOptions pagingOptions,
                                              CancellationToken cancellationToken = default)
    {
        var query = new RetrieveProductsQuery(pagingOptions);

        var response = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return new OkObjectResult(response);
    }
}