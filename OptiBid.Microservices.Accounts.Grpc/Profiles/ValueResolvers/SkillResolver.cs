using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles.ValueResolvers
{
    public class SkillsResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, ICollection<Domain.Entities.Skill>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkillsResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public ICollection<Skill> Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, ICollection<Skill> destMember, ResolutionContext context)
        {
            var availableProfessions = _unitOfWork._professionRepository.GetAll();
            List<Skill> skillList = new List<Skill>();

            foreach (var sourceSkill in source.Skills)
            {
                var selectedProfession = availableProfessions.FirstOrDefault(x => x.ID == sourceSkill.ProfessionId);
                var newSkill = new Skill()
                {
                    Profession = selectedProfession,
                    ProfessionID = selectedProfession?.ID,
                    ID = sourceSkill.SkillId,
                    IsActive = sourceSkill.IsActive,
                };
                skillList.Add(newSkill);
            }

            return skillList;
        }
    }
}
