using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Domain.DTOs;
using AutoMapper;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    internal class GetAccountByUsernameHandler : IRequestHandler<GetAccountByUsernameCommand, (bool, Domain.DTOs.UserDetails)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountByUsernameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(bool, UserDetails)> Handle(GetAccountByUsernameCommand request, CancellationToken cancellationToken)
        {
            var obj = await _unitOfWork._usersRepository.GetByUsername(request.Username, cancellationToken);
            return obj == null ?
                (false, null) :
                (true, _mapper.Map<Domain.DTOs.UserDetails>(obj));
        }
    }
}
