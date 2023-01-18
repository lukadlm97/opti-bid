using MediatR;
using AutoMapper;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using User = OptiBid.Microservices.Accounts.Domain.DTOs.User;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CreateAccountHandler: IRequestHandler<CreateAccountCommand, Domain.DTOs.User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccountHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<User> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork._usersRepository.RegisterUser(request.User, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<Domain.DTOs.User>(request.User);
        }
    }
}
