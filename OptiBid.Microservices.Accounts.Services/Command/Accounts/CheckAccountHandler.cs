using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using OptiBid.Microservices.Accounts.Services.Utility;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CheckAccountHandler : IRequestHandler<CheckAccountCommand,(bool,Domain.DTOs.UserDetails)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckAccountHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(bool, Domain.DTOs.UserDetails)> Handle(CheckAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await  _unitOfWork._usersRepository.GetByUsername(request.Username,cancellationToken);
            if (user == null || !request.Password.ValidatePassword(user.PasswordHash))
            {
                return (false,null);
            }

            return (true, _mapper.Map<Domain.DTOs.UserDetails>(await _unitOfWork._usersRepository.GetById(user.ID,cancellationToken)));
        }
    }
}
