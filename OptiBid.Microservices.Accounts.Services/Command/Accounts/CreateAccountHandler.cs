using MediatR;
using AutoMapper;
using OptiBid.Microservices.Accounts.Messaging.Send.Models;
using OptiBid.Microservices.Accounts.Messaging.Send.Sender;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using User = OptiBid.Microservices.Accounts.Domain.DTOs.User;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CreateAccountHandler: IRequestHandler<CreateAccountCommand, Domain.DTOs.User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountSender _accountSender;

        public CreateAccountHandler(IUnitOfWork unitOfWork,IMapper mapper,IAccountSender accountSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountSender = accountSender;
        }
        public async Task<User> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork._usersRepository.RegisterUser(request.User, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            _accountSender.Send(new AccountMessage()
            {
                Name = request.User.FirstName+" "+request.User.LastName,
                RoleName = request.User.UserRole.Name,
                UserName = request.User.Username
            });
            return _mapper.Map<Domain.DTOs.User>(request.User);
        }
    }
}
