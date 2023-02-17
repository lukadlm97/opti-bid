﻿
namespace OptiBid.Microservices.Contracts.Domain.Input
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<ContactRequest> Contacts { get; set; }
        public IEnumerable<SkillRequest> Skills { get; set; }
    }
}
