using Microsoft.IdentityModel.Tokens;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Infrastructure.Services
{
    public class AuctionAssetService:IAuctionAssetService
    {
        private readonly IAuctionAssetsGrpcService _auctionGrpcService;
        private readonly IHybridCache<Asset> _hybridCache;
        private readonly IFireForget<Asset> _fireForget;

        public AuctionAssetService(IAuctionAssetsGrpcService auctionGrpcService, IHybridCache<Asset> hybridCache, IFireForget<Asset> fireForget)
        {
            this._auctionGrpcService = auctionGrpcService;
            this._hybridCache = hybridCache;
            this._fireForget = fireForget;
        }
        public async Task<OperationResult<Asset>> GetAssets(PagingRequest pagingRequest, CancellationToken cancellationToken = default)
        {
            var assets = await _auctionGrpcService.Get(pagingRequest, cancellationToken);

            if (assets.IsNullOrEmpty())
            {
                return new OperationResult<Asset>(null, null, OperationResultStatus.NotFound, null);
            }

            return new OperationResult<Asset>(null, assets, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<Asset>> GetAssetById(int id, CancellationToken cancellationToken = default)
        {
            var key = nameof(Asset)+id;
            Asset asset = await _hybridCache.Get(key, cancellationToken);
            if (asset != null)
            {
                return 
                    new OperationResult<Asset>(asset, null, OperationResultStatus.Success, null);
            }

            asset = await _auctionGrpcService.Get(id,cancellationToken);
            if (asset == null)
            {
                return new OperationResult<Asset>(null, null, OperationResultStatus.NotFound, null);
            }
            if (asset != null)
            {
                _fireForget
                    .Execute(x =>
                        x.Set(key, asset, cancellationToken));
            }

            return
                new OperationResult<Asset>(asset, null, OperationResultStatus.Success, null);
        }
    }
}
