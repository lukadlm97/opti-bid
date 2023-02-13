using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;

namespace OptiBid.Microservices.Services.GrpcServices
{
    public class AccountEnumerationGrpcService:IAccountEnumerationGrpcService
    {
        private readonly IAccountGrpcFactory _accountGrpcFactory;

        public AccountEnumerationGrpcService(IAccountGrpcFactory accountGrpcFactory)
        {
            _accountGrpcFactory = accountGrpcFactory;
        }
        public async Task<IEnumerable<EnumItem>> GetCountries(CancellationToken cancellationToken = default)
        {
            var grpcClient = _accountGrpcFactory.GetDashboardClient();
            var countries = await grpcClient.GetAllCountriesAsync(new EmptyRequest() { });
            return countries.Countries.Select(x => new EnumItem() { ID = x.Id, Name = x.Name });
        }

        public Task<IEnumerable<EnumItem>> GetContactTypes(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnumItem>> GetProfessions(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnumItem>> GetUserRoles(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
