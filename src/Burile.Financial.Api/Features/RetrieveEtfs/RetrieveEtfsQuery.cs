using MediatR;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

public sealed class RetrieveEtfsQuery : IRequest<string>;