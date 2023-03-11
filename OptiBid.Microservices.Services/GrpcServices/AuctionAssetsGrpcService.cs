using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;
using OptiBid.Microservices.Services.GrpcServices;

namespace OptiBid.Microservices.Infrastructure.GrpcServices
{
    public class AuctionAssetsGrpcService:IAuctionAssetsGrpcService
    {
        private readonly IAuctionGrpcFactory _auctionGrpcFactory;
        private readonly ILogger<AuctionAssetsGrpcService> _logger;

        public AuctionAssetsGrpcService(IAuctionGrpcFactory auctionGrpcFactory, ILogger<AuctionAssetsGrpcService> logger)
        {
            _auctionGrpcFactory = auctionGrpcFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<Asset>> Get(PagingRequest pagingRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var users = await grpcClient.GetAllAsync(new AssetsRequest()
                {
                    Page = pagingRequest.page,
                    PageSize = pagingRequest.size
                },cancellationToken:cancellationToken);
                return users.Assets.Select(x => new Asset(x.Id, x.Name, 0, 0, x.Description));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return Array.Empty<Asset>();
        }

        public async Task<Asset> Get(int id,CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var user = await grpcClient.GetByIdAsync(new SingleAssetsRequest()
                {
                    Id = id
                }, cancellationToken: cancellationToken);
                if (user.Status == OperationCompletionStatus.Success && user.Asset!=null)
                {
                    return new Asset(user.Asset.Id, user.Asset.Name, 0, 0, user.Asset.Description);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return null;
        }
    }
}
