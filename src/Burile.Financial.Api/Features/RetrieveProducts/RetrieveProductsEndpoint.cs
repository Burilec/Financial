using Burile.Financial.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.RetrieveProducts;

[ApiController]
[Route("api")]
public class RetrieveProductsEndpoint : ControllerBase
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