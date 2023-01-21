using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.UserRole
{
    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRolesCommand, IEnumerable<Domain.DTOs.UserRoles>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<Domain.DTOs.UserRoles>> Handle(GetUserRolesCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Domain.Entities.UserRole>, IEnumerable<Domain.DTOs.UserRoles>>(this._unitOfWork._userRolesRepository.GetAll().AsEnumerable());
        }
    }
}
