using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Accounts.Grpc.UserServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;
using UserRequest = OptiBid.Microservices.Contracts.Domain.Input.UserRequest;

namespace OptiBid.Microservices.Services.GrpcServices
{
    public class UserGrpcService:IAccountGrpcService
    {
        private readonly ILogger<UserGrpcService> _logger;
        private readonly IAccountGrpcFactory _accountGrpcFactory;

        public UserGrpcService(ILogger<UserGrpcService> logger, IAccountGrpcFactory accountGrpcFactory)
        {
            _logger = logger;
            _accountGrpcFactory = accountGrpcFactory;
        }
        public async Task<UserResult> SignIn(string username, string password,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _accountGrpcFactory.GetUserClient();
                var reply = await grpcClient.SignInAsync(new Accounts.Grpc.UserServiceDefinition.UserRequest()
                {
                    Username = username,
                    Password = password
                });



                return reply.Status switch
                {
                    OperationCompletionStatus.Success => new UserResult()
                    {
                        Id = reply.User.Id
                    },
                    OperationCompletionStatus.BadRequest=>new UserResult()
                    {
                        Id=-1
                    },
                    OperationCompletionStatus.NotFound=>new UserResult()
                    {
                        Id =0
                    },
                    _=>null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public Task<AssetsResult> GetAssets(string username, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmFirstSignIn(string username, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(UserRequest userRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
