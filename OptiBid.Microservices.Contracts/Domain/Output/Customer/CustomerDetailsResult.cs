
namespace OptiBid.Microservices.Contracts.Domain.Output.Customer
{
    public record CustomerDetailsResult(
        int Id,
    int UserId,
    string Username,
    string DateOpened);
}
