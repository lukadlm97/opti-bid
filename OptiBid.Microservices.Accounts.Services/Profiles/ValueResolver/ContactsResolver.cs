using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Domain.Input;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Profiles.ValueResolver
{
 
    public class ContactsResolver : IValueResolver<Domain.Input.RegisterAccountModel, Domain.Entities.User, ICollection<Domain.Entities.Contact>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactsResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public ICollection<Contact> Resolve(RegisterAccountModel source, User destination, ICollection<Contact> destMember, ResolutionContext context)
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
                    ID = sourceContact.ContactId
                };
                contacts.Add(newContact);
            }

            return contacts;
        }
    }
}
