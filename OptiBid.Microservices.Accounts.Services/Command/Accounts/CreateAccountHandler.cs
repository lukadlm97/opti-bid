using MediatR;
using AutoMapper;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using OptiBid.Microservices.Accounts.Services.Utility;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using User = OptiBid.Microservices.Accounts.Domain.DTOs.User;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CreateAccountHandler: IRequestHandler<CreateAccountCommand, Domain.DTOs.User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFireForgetHandler _fireForgetHandler;

        public CreateAccountHandler(IUnitOfWork unitOfWork,IMapper mapper,IFireForgetHandler fireForgetHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fireForgetHandler = fireForgetHandler;
        }
        public async Task<User> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork._usersRepository.RegisterUser(request.User, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            _fireForgetHandler.Execute(x=>x.Send(new AccountMessage()
            {
                Name = request.User.FirstName + " " + request.User.LastName,
                RoleName = request.User.UserRole.Name,
                UserName = request.User.Username
            }));

            return _mapper.Map<Domain.DTOs.User>(request.User);
        }
    }
}
