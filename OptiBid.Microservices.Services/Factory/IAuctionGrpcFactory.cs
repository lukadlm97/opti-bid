using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Auction.Grpc.CategoriesServiceDefinition;

namespace OptiBid.Microservices.Services.Factory
{
    public interface IAuctionGrpcFactory
    {
        Category.CategoryClient GetCategoryClient();
    }
}
