using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;
using BidRequest = OptiBid.Microservices.Contracts.Domain.Input.BidRequest;

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
                var assets = await grpcClient.GetAllAsync(new AssetsRequest()
                {
                    Page = pagingRequest.page,
                    PageSize = pagingRequest.size
                },cancellationToken:cancellationToken);
                return assets.Assets.Select(x => new Asset(x.Id, x.Name, 0, 0, x.Description));
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
                var assetReply = await grpcClient.GetByIdAsync(new SingleAssetsRequest()
                {
                    Id = id
                }, cancellationToken: cancellationToken);
                if (assetReply.Status == OperationCompletionStatus.Success && assetReply.Asset!=null)
                {
                    return new Asset(assetReply.Asset.Id, assetReply.Asset.Name, 0, 0, assetReply.Asset.Description);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return null;
        }

        public async Task<UpsertResult> Insert(UpsertAssetRequest assetRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var assetReplay = await grpcClient.AddAsync(new AddAssetRequest()
                {
                    Closed = assetRequest.Closed,
                    CustomerId = assetRequest.CustomerId,
                    Description = assetRequest.Description,
                    EndDate = Timestamp.FromDateTime(assetRequest.EndDate.ToUniversalTime()),
                    MediaUrl = { assetRequest.MediaUrls },
                    Name = assetRequest.Name,
                    StartDate = Timestamp.FromDateTime(assetRequest.StartDate.ToUniversalTime()),
                    Started = assetRequest.Started,
                    ProductTypeId = assetRequest.ProductTypeId,
                    ServiceTypeId = assetRequest.ServiceTypeId,
                }, cancellationToken: cancellationToken);
                if (assetReplay.Status == OperationCompletionStatus.Success && assetReplay.AssetId != null)
                {
                    return new UpsertResult()
                    {
                        AssetId = assetReplay.AssetId
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return null;
        }

        public async Task<UpsertResult> Update(int id, UpsertAssetRequest assetRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var assetReplay = await grpcClient.UpdateAsync(new AddAssetRequest()
                {
                    AssetId = id,
                    Closed = assetRequest.Closed,
                    CustomerId = assetRequest.CustomerId,
                    Description = assetRequest.Description,
                    EndDate = Timestamp.FromDateTime(assetRequest.EndDate.ToUniversalTime()),
                    MediaUrl = { assetRequest.MediaUrls },
                    Name = assetRequest.Name,
                    StartDate = Timestamp.FromDateTime(assetRequest.StartDate.ToUniversalTime()),
                    Started = assetRequest.Started,
                    ProductTypeId = assetRequest.ProductTypeId,
                    ServiceTypeId = assetRequest.ServiceTypeId,
                }, cancellationToken: cancellationToken);
                if (assetReplay.Status == OperationCompletionStatus.Success && assetReplay.AssetId != null)
                {
                    return new UpsertResult()
                    {
                        AssetId = assetReplay.AssetId
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return null;
        }

        public async Task<UpsertResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var assetReplay = await grpcClient.DeleteAsync(new SingleAssetsRequest()
                {
                    Id = id
                },cancellationToken:cancellationToken);
                if (assetReplay.Status == OperationCompletionStatus.Success && assetReplay.AssetId != null)
                {
                    return new UpsertResult()
                    {
                        AssetId = assetReplay.AssetId
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return null;
        }

        public async Task<BidReply> AddBid(int assetId, string username, BidRequest bidRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var assetReplay = await grpcClient.AddBidAsync(new Auction.Grpc.AuctionAssetsServiceDefinition.BidRequest()
                {
                    AuctionAssetId = assetId,
                    CustomerUsername = username,
                    BidDate = Timestamp.FromDateTime(bidRequest.BidDate.ToUniversalTime()),
                    BidPrice = (float)bidRequest.BidPrice
                }, cancellationToken: cancellationToken);
                if (assetReplay.Status == OperationCompletionStatus.Success && assetReplay.SingleBid != null)
                {
                    return new BidReply()
                    {
                        BidId = assetReplay.SingleBid.BidId
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return null;
        }

        public async Task<IEnumerable<BidDetailsReply>> GetBids(int assetId, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetAuctionAssetsClient();
                var assetReplay = await grpcClient.GetBidsAsync(new SingleAssetsRequest()
                {
                    Id = assetId
                }, cancellationToken: cancellationToken);
                if (assetReplay.Status ==OperationCompletionStatus.Success && assetReplay.SingleBids!=null)
                {
                    return assetReplay.SingleBids.Select(x => new BidDetailsReply()
                    {
                        CustomerId = Convert.ToInt32(x.CustomerID),
                        BidPrice = (decimal)x.BidPrice,
                        AuctionAssetsId = x.AuctionAssetId,
                        BidDate = x.BidDate.ToDateTime(),
                        BidId = x.BidId
                    });
                }

                return Array.Empty<BidDetailsReply>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return Array.Empty<BidDetailsReply>();

        }
    }
}
