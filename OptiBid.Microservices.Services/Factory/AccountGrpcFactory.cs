using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition;
using OptiBid.Microservices.Contracts.Configuration;

namespace OptiBid.Microservices.Services.Factory
{
    public class AccountGrpcFactory:IAccountGrpcFactory
    {
        private readonly ExternalGrpcSettings _grpcSettings;
        private  Dashboard.DashboardClient _dashboardClient;


        public AccountGrpcFactory(IOptions<ExternalGrpcSettings> options)
        {
            this._grpcSettings = options.Value;
            CreateDashboardClient();
        }

        private void CreateDashboardClient()
        {
            _dashboardClient = new Dashboard.DashboardClient(GrpcChannel.ForAddress(_grpcSettings.AccountServiceUrl));
        }

        public Dashboard.DashboardClient GetDashboardClient()
        {
            if (_dashboardClient == null)
            {
                CreateDashboardClient();
            }

            return _dashboardClient;
        }
    }
}
