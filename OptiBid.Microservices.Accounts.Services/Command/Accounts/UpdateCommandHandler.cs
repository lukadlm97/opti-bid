using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class UpdateCommandHandler : IRequestHandler<UpdateAccountCommand, Domain.DTOs.User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Domain.DTOs.User> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork._usersRepository.GetById(request.UserId, cancellationToken);
            if (existingUser != null)
            {
                existingUser.FirstName=request.User.FirstName;
                existingUser.LastName=request.User.LastName;
                existingUser.CountryID = request.User.CountryID;
                existingUser.Country =
                    _unitOfWork._countryRepository.GetAll().First(x => x.ID == request.User.CountryID);
                existingUser.Skills = MapSkills(request.User.Skills, existingUser.Skills);
                existingUser.Contacts = MapContacts(request.User.Contacts, existingUser.Contacts);

                _unitOfWork._usersRepository.UpdateUser(existingUser);
                await _unitOfWork.Commit(cancellationToken);

                return _mapper.Map<Domain.DTOs.User>(existingUser);
            }

            return null;
        }

        private ICollection<Skill> MapSkills(IEnumerable<Domain.Entities.Skill> skills,IEnumerable<Domain.Entities.Skill> existingSkills)
        {
            List<Skill> list = new List<Skill>();

            foreach (var skill in existingSkills)
            {
                if (skills.Any(x => x.ID == skill.ID))
                {
                    var foundedSkill = skills.FirstOrDefault(x =>   x.ID == skill.ID);
                    skill.ProfessionID = foundedSkill.ProfessionID;
                    skill.Profession = _unitOfWork._professionRepository.GetAll()
                        .FirstOrDefault(x => x.ID == foundedSkill.ProfessionID);
                    skill.IsActive = foundedSkill.IsActive;
                    list.Add(skill);
                }
            }

            foreach (var skill in skills.Where(x=>x.ID==0))
            {
                list.Add(new Skill()
                {
                    ProfessionID = skill.ProfessionID,
                    Profession = _unitOfWork._professionRepository.GetAll().FirstOrDefault(x=>x.ID==skill.ProfessionID)
                });
            }
            return list;
        }
        private ICollection<Contact> MapContacts(IEnumerable<Domain.Entities.Contact> contacts, IEnumerable<Domain.Entities.Contact> existingContacts)
        {
            List<Contact> list = new List<Contact>();

            foreach (var contact in existingContacts)
            {
                if (contacts.Any(x => x.ID == contact.ID))
                {
                    var foundedContact =
                        contacts.FirstOrDefault(x => x.ID == contact.ID);
                    contact.ContactTypeID = foundedContact.ContactTypeID;
                    contact.ContactType = foundedContact.ContactType;
                    contact.IsActive = foundedContact.IsActive;
                    contact.Content= foundedContact.Content;
                    list.Add(contact);
                }
            }

            foreach (var contact in contacts.Where(x=>x.ID==0))
            {
                list.Add(new Contact()
                {
                    ContactTypeID = contact.ContactTypeID,
                    ContactType = _unitOfWork._contactTypeRepository.GetAll().FirstOrDefault(x => x.ID == contact.ContactTypeID),
                    Content = contact.Content
                });
            }

            return list;
        }
    }
}
