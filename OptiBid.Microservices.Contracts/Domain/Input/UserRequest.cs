
namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public record UserRequest(
        string Email,
        string Password,
        string FirstName,
        string LastName,
        int CountryId,
        IEnumerable<ContactRequest> Contacts,
        IEnumerable<SkillRequest> Skills
    );
}
