using Burile.Financial.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

[ApiController]
[Route("api")]
public sealed class RetrieveProductsEndpoint : ControllerBase
{
    [HttpPost]
    [Route("products")]
    public async Task<IActionResult> PostAsync([FromServices] IMediator mediator,
                                               [FromQuery] PagingOptions pagingOptions,
                                               [FromBody] RetrieveProductRequest retrieveProductRequest,
                                               CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new RetrieveProductsQuery(pagingOptions, retrieveProductRequest);

            var response = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

            return new OkObjectResult(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}