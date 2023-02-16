using System.Security.Cryptography;
using MediatR;
using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class RefreshTokenHandler:IRequestHandler<RefreshTokenCommand,(bool,string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RefreshTokenHandler> _logger;

        public RefreshTokenHandler(IUnitOfWork unitOfWork,ILogger<RefreshTokenHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<(bool, string)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork._usersRepository.GetByUsername(request.Username, cancellationToken);
            if (user == null)
            {
                return (false, null);
            }
            var userAssets = await _unitOfWork._accountAssetsRepository.Get(user.ID, cancellationToken);
            if (userAssets.Item1 == request.RefreshToken)
            {
                await _unitOfWork._accountAssetsRepository.InvalidateTokens(user.ID, cancellationToken);
                var token = CreateRefreshToken();
                await _unitOfWork._accountAssetsRepository.AddRefreshToken(new UserToken()
                {
                    RefreshToken = token,
                    User = user,
                    IsValid = false
                },cancellationToken);

                await _unitOfWork.Commit(cancellationToken);
                return (true, token);
            }

            return (false, null);

        }
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
