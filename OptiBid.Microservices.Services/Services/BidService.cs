using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Infrastructure.Services
{
    public class BidService:IBidService
    {
        private readonly IAuctionAssetsGrpcService _auctionGrpcService;
        private readonly IHybridCache<BidDetailsReply> _hybridCache;
        private readonly IFireForget<Asset> _fireForgetAsset;
        private readonly IFireForget<BidDetailsReply> _fireForgetBids;

        public BidService(IAuctionAssetsGrpcService auctionGrpcService, IHybridCache<BidDetailsReply> hybridCache, IFireForget<Asset> fireForgetAsset, IFireForget<BidDetailsReply> fireForgetBids)
        {
            this._auctionGrpcService = auctionGrpcService;
            this._hybridCache = hybridCache;
            this._fireForgetAsset = fireForgetAsset;
            this._fireForgetBids = fireForgetBids;
        }
        public async Task<OperationResult<BidReply>> Add(int assetId, string username, BidRequest bidRequest, CancellationToken cancellationToken = default)
        {
            var key = nameof(Asset) + assetId;
            _fireForgetAsset
                .Execute(x =>
                    x.Invalidate(key, cancellationToken));
            var result = await _auctionGrpcService.AddBid(assetId, username, bidRequest, cancellationToken);

            if (result != null)
            {
                return new OperationResult<BidReply>(result, null, OperationResultStatus.Success, null);
            }

            return new OperationResult<BidReply>(null, null, OperationResultStatus.BadRequest, null);
        }

        public async Task<OperationResult<BidDetailsReply>> Get(int assetId, CancellationToken cancellationToken = default)
        {
            var key = nameof(BidDetailsReply) + assetId;
            IEnumerable<BidDetailsReply> items = await _hybridCache.GetCollection(key,cancellationToken);
            if (items != null)
            {
                return new OperationResult<BidDetailsReply>(null, items, OperationResultStatus.Success, null);
            }

            items = await _auctionGrpcService.GetBids(assetId, cancellationToken);

            if (items == null)
            {
                return new OperationResult<BidDetailsReply>(null, null, OperationResultStatus.NotFound, null);
            }
            else
            {
                _fireForgetBids
                    .Execute(x =>
                        x.Set(key, items.ToList(), cancellationToken));
            }


            return new OperationResult<BidDetailsReply>(null, items, OperationResultStatus.Success, null);
        }
    }
}
