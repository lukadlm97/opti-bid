using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.GrpcServices;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Services.Services
{
    public class AccountDashboardService:IAccountDashboardService
    {
        private readonly IAccountEnumerationGrpcService _accountEnumerationGrpcService;
        private readonly IHybridCache<EnumItem> _hybridCache;
        private readonly IFireForget<EnumItem> _fireForgetHandler;

        public AccountDashboardService(IAccountEnumerationGrpcService  accountEnumerationGrpcService,
            IHybridCache<EnumItem> hybridCache,
            IFireForget<EnumItem> fireForgetHandler)
        {
            _accountEnumerationGrpcService = accountEnumerationGrpcService;
            _hybridCache=hybridCache;
            _fireForgetHandler = fireForgetHandler;
        }

        public async Task<OperationResult<EnumItem>> GetProfessions(CancellationToken cancellationToken)
        {
            var key = "professions";
            IEnumerable<EnumItem> professions = await _hybridCache.GetCollection(key, cancellationToken);
            if (professions != null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = professions,
                    Status = OperationResultStatus.Success
                };
            }

            professions = await _accountEnumerationGrpcService.GetProfessions(cancellationToken);
            if (professions == null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = professions,
                    Status = OperationResultStatus.NotFound
                };
            }
            if (professions != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, professions.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>()
            {
                Collection = professions,
                Status = OperationResultStatus.Success
            };
        }

        public async Task<OperationResult<EnumItem>> GetContactTypes(CancellationToken cancellationToken)
        {
            var key = "contactTypes";
            IEnumerable<EnumItem> types = await _hybridCache.GetCollection(key, cancellationToken);
            if (types != null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = types,
                    Status = OperationResultStatus.Success
                };
            }

            types = await _accountEnumerationGrpcService.GetContactTypes(cancellationToken);
            if (types == null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = types,
                    Status = OperationResultStatus.NotFound
                };
            }

            if (types != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, types.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>()
            {
                Collection = types,
                Status = OperationResultStatus.Success
            };
        }

        public async Task<OperationResult<EnumItem>> GetUserRoles(CancellationToken cancellationToken)
        {
            var key = "roles";
            IEnumerable<EnumItem> roles = await _hybridCache.GetCollection(key, cancellationToken);
            if (roles != null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = roles,
                    Status = OperationResultStatus.Success
                };
            }

            roles = await _accountEnumerationGrpcService.GetUserRoles(cancellationToken);
            if (roles == null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = roles,
                    Status = OperationResultStatus.NotFound
                };
            }
            if (roles != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, roles.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>()
            {
                Collection = roles,
                Status = OperationResultStatus.Success
            };
        }

        public async  Task<OperationResult<EnumItem>> GetCountries(CancellationToken cancellationToken)
        {
            var key = "countries";
            IEnumerable<EnumItem> countries = await _hybridCache.GetCollection(key, cancellationToken);
            if (countries != null)
            {
                return new OperationResult<EnumItem>()
                {
                  Collection = countries,
                  Status = OperationResultStatus.Success
                };
            }

            countries = await _accountEnumerationGrpcService.GetCountries(cancellationToken);
            if (countries == null)
            {
                return new OperationResult<EnumItem>()
                {
                    Collection = countries,
                    Status = OperationResultStatus.NotFound
                };
            }
            if (countries != null)
            {
                _fireForgetHandler.Execute(x => x.Set(key, countries.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>()
            {
                Collection = countries,
                Status = OperationResultStatus.Success
            };
        }
    }
}
