using Burile.Financial.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolios;

[ApiController]
[Route("api")]
public class RetrievePortfoliosEndpoint : ControllerBase
{
    [HttpGet]
    [Route("portfolios")]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              [FromQuery] PagingOptions pagingOptions,
                                              CancellationToken cancellationToken = default)
    {
        var query = new RetrievePortfoliosQuery(pagingOptions);

        var response = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return new OkObjectResult(response);
    }
}