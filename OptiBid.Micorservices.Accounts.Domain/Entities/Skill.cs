

namespace OptiBid.Microservices.Accounts.Domain.Entities
{
    public class Skill
    {
        public int ID { get; set; }
        public bool IsActive { get; set; } = true;
        public User? User { get; set; }
        public int? UserID { get; set; }
        public Profession? Profession { get; set; }
        public int? ProfessionID { get; set; }
    }
}
