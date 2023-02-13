using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.GrpcServices;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Services.Services
{
    public class AccountDashboardService:IAccountDashboardService
    {
        private readonly IAccountEnumerationGrpcService _accountEnumerationGrpcService;
        private readonly IHybridCache<EnumItem> _hybridCache;

        public AccountDashboardService(IAccountEnumerationGrpcService  accountEnumerationGrpcService,IHybridCache<EnumItem> hybridCache)
        {
            _accountEnumerationGrpcService = accountEnumerationGrpcService;
            _hybridCache=hybridCache;
        }
        public async Task<IEnumerable<EnumItem>> GetCountries(CancellationToken cancellationToken)
        {
            var key = "countries";
            IEnumerable<EnumItem> countries = await _hybridCache.GetCollection(key, cancellationToken);
            if (countries != null)
            {
                return countries;
            }

            countries = await _accountEnumerationGrpcService.GetCountries(cancellationToken);
            if (countries != null)
            {
                await _hybridCache.Set(key, countries.ToList(), cancellationToken);
            }

            return countries;
        }
    }
}
