namespace OptiBid.Microservices.Accounts.Domain.Input
{
    public class RegisterAccountModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<ContactModel> Contacts { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }
    }
}
