using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Domain.Input;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Profiles.ValueResolver
{
    public class SkillsResolver:IValueResolver<Domain.Input.RegisterAccountModel,Domain.Entities.User,ICollection<Domain.Entities.Skill>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkillsResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public ICollection<Skill> Resolve(RegisterAccountModel source, User destination, ICollection<Skill> destMember, ResolutionContext context)
        {
            var availableProfessions = _unitOfWork._professionRepository.GetAll();
            List<Skill> skillList = new List<Skill>();

            foreach (var sourceSkill in source.Skills)
            {
                var selectedProfession = availableProfessions.FirstOrDefault(x => x.ID == sourceSkill.ProfessionId);
                var newSkill = new Skill()
                {
                    Profession = selectedProfession,
                    ProfessionID = selectedProfession?.ID
                };
                skillList.Add(newSkill);
            }

            return skillList;
        }
    }
}
