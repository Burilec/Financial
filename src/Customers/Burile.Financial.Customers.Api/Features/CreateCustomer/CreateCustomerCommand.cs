using MediatR;

namespace Burile.Financial.Customers.Api.Features.CreateCustomer;

public sealed record CreateCustomerCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest<CustomerCreatedResponse>;