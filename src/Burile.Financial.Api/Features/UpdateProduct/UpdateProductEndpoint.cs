using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.UpdateProduct;

[ApiController]
[Route("api")]
public sealed class UpdateProductEndpoint : ControllerBase
{
    [HttpPut]
    [Route("products/{apiId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              [FromRoute] Guid apiId,
                                              [FromBody] UpdateProductRequest updateProductRequest,
                                              CancellationToken cancellationToken = default)
    {
        var command = new UpdateProductCommand(apiId, updateProductRequest);

        var result = await mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return result.IsSuccess
            ? new OkResult()
            : new ConflictObjectResult(result.ToString());
    }
}