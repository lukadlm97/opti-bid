using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles.ValueResolvers
{
    public class ContactsResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, ICollection<Domain.Entities.Contact>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactsResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public ICollection<Contact> Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, ICollection<Contact> destMember, ResolutionContext context)
        {
            var availableContactTypes = _unitOfWork._contactTypeRepository.GetAll();
            List<Contact> contacts = new List<Contact>();

            foreach (var sourceContact in source.Contacts)
            {
                var seContactType = availableContactTypes.FirstOrDefault(x => x.ID == sourceContact.ContactTypeId);
                var newContact = new Contact()
                {
                    ContactType = seContactType,
                    ContactTypeID = seContactType?.ID,
                    Content = sourceContact.Content,
                    ID = sourceContact.ContactId,
                    IsActive = sourceContact.IsActive,
                };
                contacts.Add(newContact);
            }

            return contacts;
        }
    }
}
