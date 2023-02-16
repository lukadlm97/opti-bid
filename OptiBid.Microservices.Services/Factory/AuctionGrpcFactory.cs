using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Auction.Grpc.CategoriesServiceDefinition;
using OptiBid.Microservices.Contracts.Configuration;
using static OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition.Dashboard;

namespace OptiBid.Microservices.Services.Factory
{
    public class AuctionGrpcFactory:IAuctionGrpcFactory
    {
        private ExternalGrpcSettings _grpcSettings;
        private Category.CategoryClient _categoryClient;
        public  AuctionGrpcFactory(IOptions<ExternalGrpcSettings> grpcSettings)
        {
            _grpcSettings = grpcSettings.Value;
            CreateCategoryClient();
        }
        private void CreateCategoryClient()
        {
            _categoryClient = new Category.CategoryClient(GrpcChannel.ForAddress(_grpcSettings.AuctionServiceUrl));
        }
        public Category.CategoryClient GetCategoryClient()
        {
            if (_categoryClient == null)
            {
                CreateCategoryClient();
            }

            return _categoryClient;
        }
    }
}
