using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition;
using OptiBid.Microservices.Accounts.Grpc.UserServiceDefinition;
using OptiBid.Microservices.Contracts.Configuration;

namespace OptiBid.Microservices.Services.Factory
{
    public class AccountGrpcFactory:IAccountGrpcFactory
    {
        private readonly ExternalGrpcSettings _grpcSettings;
        private  Dashboard.DashboardClient _dashboardClient;
        private User.UserClient _userClient;


        public AccountGrpcFactory(IOptions<ExternalGrpcSettings> options)
        {
            this._grpcSettings = options.Value;
            CreateDashboardClient();
            CreateUserClient();
        }

        private void CreateDashboardClient()
        {
            _dashboardClient = new Dashboard.DashboardClient(GrpcChannel.ForAddress(_grpcSettings.AccountServiceUrl));
        }
        private void CreateUserClient()
        {
            _userClient = new User.UserClient(GrpcChannel.ForAddress(_grpcSettings.AccountServiceUrl));
        }

        public Dashboard.DashboardClient GetDashboardClient()
        {
            if (_dashboardClient == null)
            {
                CreateDashboardClient();
            }

            return _dashboardClient;
        }

        public User.UserClient GetUserClient()
        {
            if (_userClient == null)
            {
                CreateUserClient();
            }

            return _userClient;
        }
    }
}
