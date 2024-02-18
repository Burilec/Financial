using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolio;

[ApiController]
[Route("api", Name = nameof(RetrievePortfolioEndpoint))]
public class RetrievePortfolioEndpoint : ControllerBase
{
    [HttpGet]
    [Route("portfolios/{apiId:guid}", Name = nameof(GetPortfolioAsync))]
    public async Task<IActionResult> GetPortfolioAsync([FromServices] IMediator mediator,
                                                       [FromRoute] Guid apiId,
                                                       CancellationToken cancellationToken = default)
    {
        var query = new RetrievePortfolioQuery(apiId);

        var result = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return result.IsSuccess
            ? new OkObjectResult(result.Value)
            : new NoContentResult();
    }
}