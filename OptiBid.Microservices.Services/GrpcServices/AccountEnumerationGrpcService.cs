using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;

namespace OptiBid.Microservices.Services.GrpcServices
{
    public class AccountEnumerationGrpcService:IAccountEnumerationGrpcService
    {
        private readonly IAccountGrpcFactory _accountGrpcFactory;
        private readonly ILogger<AccountEnumerationGrpcService> _logger;

        public AccountEnumerationGrpcService(IAccountGrpcFactory accountGrpcFactory,ILogger<AccountEnumerationGrpcService> logger)
        {
            _accountGrpcFactory = accountGrpcFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<EnumItem>> GetCountries(CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _accountGrpcFactory.GetDashboardClient();
                var countries = await grpcClient.GetAllCountriesAsync(new EmptyRequest() { });
                return countries.Countries.Select(x => new EnumItem() { ID = x.Id, Name = x.Name });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<EnumItem>> GetContactTypes(CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _accountGrpcFactory.GetDashboardClient();
                var contactTypes = await grpcClient.GetAllContactTypesAsync(new EmptyRequest() { });
                return contactTypes.ContactTypes.Select(x => new EnumItem()
                {
                    ID = x.Id,
                    Name = x.Name,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
           
        }

        public async Task<IEnumerable<EnumItem>> GetProfessions(CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _accountGrpcFactory.GetDashboardClient();
                var professions = await grpcClient.GetAllProfessionsAsync(new EmptyRequest() { });
                return professions.Professions.Select(x => new EnumItem()
                {
                    ID = x.Id,
                    Name = x.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
           
        }

        public async Task<IEnumerable<EnumItem>> GetUserRoles(CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _accountGrpcFactory.GetDashboardClient();
                var reply = await grpcClient.GetAllUserRolesAsync(new EmptyRequest() { });
                return reply.UserRoles.Select(x => new EnumItem()
                {
                    ID = x.Id,
                    Name = x.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            
        }
    }
}
