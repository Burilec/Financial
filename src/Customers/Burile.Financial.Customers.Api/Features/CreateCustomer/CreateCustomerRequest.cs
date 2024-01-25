namespace Burile.Financial.Customers.Api.Features.CreateCustomer;

public record CreateCustomerRequest(string FirstName, string LastName, string Email, string Password);