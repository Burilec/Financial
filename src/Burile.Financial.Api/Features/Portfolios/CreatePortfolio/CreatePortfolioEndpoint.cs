using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Portfolios.CreatePortfolio;

[ApiController]
[Route("api")]
public sealed class CreatePortfolioEndpoint : ControllerBase
{
    [HttpPost]
    [Route("portfolios")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromServices] IMediator mediator,
                                               [FromBody] CreatePortfolioRequest request,
                                               CancellationToken cancellationToken = default)
    {
        if (request == null)
            return BadRequest();

        var command = new CreatePortfolioCommand(request);

        var response = await mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return Created($"api/portfolios/{response.ApiId}", response);
    }
}