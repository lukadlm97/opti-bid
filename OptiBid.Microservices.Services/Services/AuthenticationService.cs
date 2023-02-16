using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;

namespace OptiBid.Microservices.Services.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IAccountGrpcService _accountGrpcService;

        public AuthenticationService(IAccountGrpcService accountGrpcService)
        {
            _accountGrpcService = accountGrpcService;
        }
        public async Task<OperationResult<string>> SignIn(string username, string password, CancellationToken cancellationToken = default)
        {
            var userResult = await _accountGrpcService.SignIn(username, password, cancellationToken);
            if (userResult == null)
            {
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.BadRequest
                };
            }
            if (userResult.Id == -1)
            {
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.BadRequest
                };
            }

            if (userResult.Id==0)
            {
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.NotFound
                };
            }

            return new OperationResult<string>()
            {
                Status = OperationResultStatus.Success
            };
        }

        public Task<OperationResult<string>> Register(UserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<string>> RenewToken(string username, string refreshToken, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<string>> Verify(string username, string code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<string>> Validate(string username, string code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
