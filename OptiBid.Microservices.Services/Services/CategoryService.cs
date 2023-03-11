using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Services.Services
{
    public class CategoryService:ICategoryDashboardService
    {
        private readonly IAccountEnumerationGrpcService _accountEnumerationGrpcService;
        private readonly ICategoryEnumerationGrpcService _categoryEnumerationGrpcService;
        private readonly IHybridCache<EnumItem> _hybridCache;
        private readonly IFireForget<EnumItem> _fireForgetHandler;

        public CategoryService(IAccountEnumerationGrpcService accountEnumerationGrpcService,
            ICategoryEnumerationGrpcService categoryEnumerationGrpcService,
            IHybridCache<EnumItem> hybridCache,
            IFireForget<EnumItem> fireForgetHandler)
        {
            _accountEnumerationGrpcService = accountEnumerationGrpcService;
            _categoryEnumerationGrpcService = categoryEnumerationGrpcService;
            _hybridCache = hybridCache;
            _fireForgetHandler = fireForgetHandler;
        }

        public async Task<OperationResult<EnumItem>> GetProducts(CancellationToken cancellationToken = default)
        {
            var key = "products";
            IEnumerable<EnumItem> products = await _hybridCache.GetCollection(key, cancellationToken);
            if (products != null)
            {
                return new OperationResult<EnumItem>(null, products, OperationResultStatus.Success, null);

                products = await _categoryEnumerationGrpcService.GetProducts(cancellationToken);
                if (products == null)
                {
                    return new OperationResult<EnumItem>(null, null, OperationResultStatus.NotFound, null);
                }

                if (products != null)
                {
                    _fireForgetHandler
                        .Execute(x =>
                            x.Set(key, products.ToList(), cancellationToken));
                }
            }
            return new OperationResult<EnumItem>(null, products, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<EnumItem>> GetServices(CancellationToken cancellationToken = default)
        {
            var key = "services";
            IEnumerable<EnumItem> categories = await _hybridCache.GetCollection(key, cancellationToken);
            if (categories != null)
            {
                return new OperationResult<EnumItem>(null, categories, OperationResultStatus.Success, null);
                
            }

            categories = await _categoryEnumerationGrpcService.GetCategories(cancellationToken);
            if (categories == null)
            {
                return new OperationResult<EnumItem>(null, null, OperationResultStatus.NotFound, null);
              
            }
            if (categories != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, categories.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>(null, categories, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<EnumItem>> GetProfessions(CancellationToken cancellationToken)
        {
            var key = "professions";
            IEnumerable<EnumItem> professions = await _hybridCache.GetCollection(key, cancellationToken);
            if (professions != null)
            {
                return new OperationResult<EnumItem>(null, professions, OperationResultStatus.Success, null);
            }

            professions = await _accountEnumerationGrpcService.GetProfessions(cancellationToken);
            if (professions == null)
            {
                return new OperationResult<EnumItem>(null, null, OperationResultStatus.NotFound, null);
            }
            if (professions != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, professions.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>(null, professions, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<EnumItem>> GetContactTypes(CancellationToken cancellationToken)
        {
            var key = "contactTypes";
            IEnumerable<EnumItem> types = await _hybridCache.GetCollection(key, cancellationToken);
            if (types != null)
            {
                return new OperationResult<EnumItem>(null, types, OperationResultStatus.Success, null);
            }

            types = await _accountEnumerationGrpcService.GetContactTypes(cancellationToken);
            if (types == null)
            {
                return new OperationResult<EnumItem>(null, null, OperationResultStatus.NotFound, null);
            }

            if (types != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, types.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>(null, types, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<EnumItem>> GetUserRoles(CancellationToken cancellationToken)
        {
            var key = "roles";
            IEnumerable<EnumItem> roles = await _hybridCache.GetCollection(key, cancellationToken);
            if (roles != null)
            {
                return new OperationResult<EnumItem>(null, roles, OperationResultStatus.Success, null);
            }

            roles = await _accountEnumerationGrpcService.GetUserRoles(cancellationToken);
            if (roles == null)
            {
                return new OperationResult<EnumItem>(null, null, OperationResultStatus.NotFound, null);
            }
            if (roles != null)
            {
                _fireForgetHandler
                    .Execute(x =>
                        x.Set(key, roles.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>(null, roles, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<EnumItem>> GetCountries(CancellationToken cancellationToken)
        {
            var key = "countries";
            IEnumerable<EnumItem> countries = await _hybridCache.GetCollection(key, cancellationToken);
            if (countries != null)
            {
                return new OperationResult<EnumItem>(null, countries, OperationResultStatus.Success, null);
            }

            countries = await _accountEnumerationGrpcService.GetCountries(cancellationToken);
            if (countries == null)
            {
                return new OperationResult<EnumItem>(null, null, OperationResultStatus.NotFound, null);
            }
            if (countries != null)
            {
                _fireForgetHandler.Execute(x => x.Set(key, countries.ToList(), cancellationToken));
            }

            return new OperationResult<EnumItem>(null, countries, OperationResultStatus.Success, null);
        }
    }
}
