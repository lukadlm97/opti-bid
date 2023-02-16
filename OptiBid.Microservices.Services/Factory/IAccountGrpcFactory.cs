using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition;
using OptiBid.Microservices.Accounts.Grpc.UserServiceDefinition;

namespace OptiBid.Microservices.Services.Factory
{
    public interface IAccountGrpcFactory
    {
        Dashboard.DashboardClient GetDashboardClient();
        User.UserClient GetUserClient();
    }
}
