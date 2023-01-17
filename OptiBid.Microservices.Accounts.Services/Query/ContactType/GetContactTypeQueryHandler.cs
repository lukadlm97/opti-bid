using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Query.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Query.ContactType
{
    public class GetContactTypeQueryHandler : IRequestHandler<GetContactTypeCommand, List<Domain.Entities.ContactType>>
    {
        private readonly IRepository<Domain.Entities.ContactType> _contactTypeRepository;

        public GetContactTypeQueryHandler(IRepository<Domain.Entities.ContactType> contactTypeRepository)
        {
            _contactTypeRepository = contactTypeRepository;
        }

        public async Task<List<Domain.Entities.ContactType>> Handle(GetContactTypeCommand request, CancellationToken cancellationToken)
        {
            return _contactTypeRepository.GetAll().ToList();
        }
    }
}
