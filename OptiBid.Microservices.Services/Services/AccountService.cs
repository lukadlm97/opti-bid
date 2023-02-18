using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Services.Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountGrpcService _accountGrpcService;
        private readonly  IHybridCache<UserResult> _hybridCache;
        private readonly IFireForget<UserResult> _fireForget;

        public AccountService(IAccountGrpcService accountGrpcService, IHybridCache<UserResult> hybridCache,IFireForget<UserResult> fireForget)
        {
            this._accountGrpcService = accountGrpcService;
            this._hybridCache = hybridCache;
            this._fireForget = fireForget;
        }
        public async Task<OperationResult<UserResult>> GetDetails(string username, CancellationToken cancellationToken)
        {
            var userProfile = await _hybridCache.Get(username, cancellationToken);
            if (userProfile == null)
            {
                userProfile = await _accountGrpcService.GetByUsername(username, cancellationToken);
                if (userProfile == null)
                {
                    return new OperationResult<UserResult>()
                    {
                        Status = OperationResultStatus.NotFound
                    };
                }
                _fireForget.Execute(x=>x.Set(username,userProfile,cancellationToken));
            }
            return new OperationResult<UserResult>()
            {
                Data = userProfile,
                Status = OperationResultStatus.Success
            };
        }

        public async Task<OperationResult<UserResult>> GetDetails(int userId, CancellationToken cancellationToken)
        {
           
                var userProfile = await _accountGrpcService.GetById(userId, cancellationToken);
                if (userProfile == null)
                {
                    return new OperationResult<UserResult>()
                    {
                        Status = OperationResultStatus.NotFound
                    };
                }
              
            return new OperationResult<UserResult>()
            {
                Data = userProfile,
                Status = OperationResultStatus.Success
            };
        }

        public async Task<OperationResult<SingleUserResult>> Get(CancellationToken cancellationToken)
        {
            var users = await _accountGrpcService.Get(cancellationToken);
            if (users == null)
            {
                return new OperationResult<SingleUserResult>()
                {
                    Status = OperationResultStatus.NotFound
                };
            }

            return new OperationResult<SingleUserResult>()
            {
                Collection = users,
                Status = OperationResultStatus.Success
            };
        }

        public async Task<OperationResult<bool>> UpdateProfile(string username,UserRequest userRequest, CancellationToken cancellationToken)
        {
            var user = await _accountGrpcService.GetByUsername(username, cancellationToken);
            if (user == null)
            {
                return new OperationResult<bool>()
                {
                    Status = OperationResultStatus.NotFound
                };
            }
            var isChanged = await _accountGrpcService.Update(user.Id, userRequest, cancellationToken);

            if (isChanged)
            {
                return new OperationResult<bool>()
                {
                    Status = OperationResultStatus.Success,
                    Data = true
                };
            }

            return new OperationResult<bool>()
            {
                Status = OperationResultStatus.BadRequest
            };
        }
    }
}
