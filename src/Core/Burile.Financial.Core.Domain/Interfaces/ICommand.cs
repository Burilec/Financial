using FluentResults;
using MediatR;

namespace Burile.Financial.Core.Domain.Interfaces;

public interface ICommand : IRequest<Result>
{
    
}