using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.Products.RetrieveProductByApiId;

[ApiController]
[Route("api")]
public sealed class RetrieveProductByApiIdEndpoint : ControllerBase
{
    [HttpGet]
    [Route("products/{apiId:guid}")]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              [FromRoute] Guid apiId,
                                              CancellationToken cancellationToken = default)
    {
        var query = new RetrieveProductByApiIdQuery(apiId);

        var result = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return result.IsSuccess
            ? new OkObjectResult(result.Value)
            : new NoContentResult();
    }
}