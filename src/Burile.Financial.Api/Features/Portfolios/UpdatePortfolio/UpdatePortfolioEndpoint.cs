using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Portfolios.UpdatePortfolio;

[ApiController]
[Route("api")]
public sealed class UpdatePortfolioEndpoint : ControllerBase
{
    [HttpPut]
    [Route("portfolios/{apiId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              [FromRoute] Guid apiId,
                                              [FromBody] UpdatePortfolioRequest request,
                                              CancellationToken cancellationToken = default)
    {
        var command = new UpdatePortfolioCommand(apiId, request);

        var result = await mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return result.IsSuccess
            ? new OkResult()
            : new ConflictObjectResult(result.ToString());
    }
}