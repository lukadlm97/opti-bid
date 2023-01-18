using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Query.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.ContactType
{
    public class GetContactTypeQueryHandler : IRequestHandler<GetContactTypeCommand, IEnumerable<Domain.DTOs.ContactType>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetContactTypeQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.DTOs.ContactType>> Handle(GetContactTypeCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Domain.Entities.ContactType>,IEnumerable<Domain.DTOs.ContactType>>(_unitOfWork._contactTypeRepository.GetAll().AsEnumerable());
        }
    }
}
