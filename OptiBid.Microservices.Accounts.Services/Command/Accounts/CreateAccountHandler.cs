using MediatR;
using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using OptiBid.Microservices.Accounts.Services.Utility;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;
using User = OptiBid.Microservices.Accounts.Domain.DTOs.User;
using System.Security.Cryptography;

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
                UserName = request.User.Username,
                AccountMessageType = AccountMessageType.Registration
            }));

            var userId = request.User.ID;
            var user = await _unitOfWork._usersRepository.GetById(userId, cancellationToken);
            if (user == null)
            {
                return null;
            }
            await _unitOfWork._accountAssetsRepository.AddRefreshToken(new UserToken()
            {
                RefreshToken = CreateRefreshToken(),
                User = user,
                IsValid = true
            },cancellationToken);
            await _unitOfWork._accountAssetsRepository.AddTwoFAKey(new UserTwoFAAssets()
            {
                Source = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10),
                User = user
            },cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<Domain.DTOs.User>(request.User);
        }
        private  string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
