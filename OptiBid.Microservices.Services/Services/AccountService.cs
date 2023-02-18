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
    }
}
