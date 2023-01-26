using AutoMapper;
using Grpc.Core;
using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;
using OptiBid.Microservices.Auction.Services.Enumerations;
using OptiBid.Microservices.Auction.Services.Services;

namespace OptiBid.Microservices.Auction.Grpc.Services
{
    public class AuctionAssetsService:AuctionAssetsServiceDefinition.AuctionAssets.AuctionAssetsBase
    {
        private readonly IAuctionAssetService _auctionAssetService;
        private readonly IMapper _mapper;
        private readonly IBidService _bidService;

        public AuctionAssetsService(IAuctionAssetService auctionAssetService,IMapper mapper,IBidService bidService)
        {
            _auctionAssetService = auctionAssetService;
            _mapper = mapper;
            _bidService = bidService;
        }

        public override async Task<AddAssetReplay> Add(AddAssetRequest request, ServerCallContext context)
        {
            var response =
                await _auctionAssetService.Create(
                    _mapper.Map<Domain.Input.AuctionAsset>(request), context.CancellationToken);

            return response.CreationStatus switch
            {
                CreationStatus.Success => new AddAssetReplay()
                {
                    AssetId = response.Asset.Id,
                    Status = OperationCompletionStatus.Success
                },
                CreationStatus.Error => new AddAssetReplay()
                {
                    Status = OperationCompletionStatus.NotFound,
                    AssetId = -1
                },
                _ => new AddAssetReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                }
            };
        }

        public override async Task<AddAssetReplay> Delete(SingleAssetsRequest request, ServerCallContext context)
        {
            var response =
                await _auctionAssetService.Delete(request.Id, context.CancellationToken);

            return response.CreationStatus switch
            {
                CreationStatus.Success => new AddAssetReplay()
                {
                    AssetId = request.Id,
                    Status = OperationCompletionStatus.Success
                },
                CreationStatus.Error => new AddAssetReplay()
                {
                    Status = OperationCompletionStatus.NotFound,
                    AssetId = -1
                },
                _ => new AddAssetReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                }
            };
        }

        public override async Task<AssetsReply> GetAll(AssetsRequest request, ServerCallContext context)
        {
            var response = await _auctionAssetService.GetAll(context.CancellationToken);

            return response.SearchStatus switch
            {
                SearchStatus.Success => new AssetsReply()
                {
                    Assets =
                    {
                        _mapper.Map<IEnumerable<AuctionAssetsServiceDefinition.SingleAssetReply>>(response.Assets)
                    },
                    TotalAvailable = response.Assets.Count()
                },
                SearchStatus.NotFound=> new AssetsReply()
                {
                    TotalAvailable = 0
                },
                _ => new AssetsReply()
                {
                    TotalAvailable = -1
                }
            };

        }

        public override async Task<AssetReply> GetById(SingleAssetsRequest request, ServerCallContext context)
        {
            var response = await _auctionAssetService.GetById(request.Id,context.CancellationToken);

            return response.SearchStatus switch
            {
                SearchStatus.Success => new AssetReply()
                {
                    Asset =  _mapper.Map<AuctionAssetsServiceDefinition.SingleAssetReply>(response.Asset),
                    Status = OperationCompletionStatus.Success
                },
                SearchStatus.NotFound => new AssetReply()
                {
                    Status = OperationCompletionStatus.NotFound
                },
                _ => new AssetReply()
                {
                    Status = OperationCompletionStatus.BadRequest
                }
            };
        }

        public override async Task<AddAssetReplay> Update(AddAssetRequest request, ServerCallContext context)
        {
            var response =
                await _auctionAssetService.Update(
                    _mapper.Map<Domain.Input.AuctionAsset>(request), context.CancellationToken);

            return response.CreationStatus switch
            {
                CreationStatus.Success => new AddAssetReplay()
                {
                    AssetId = response.Asset.Id,
                    Status = OperationCompletionStatus.Success
                },
                CreationStatus.Error => new AddAssetReplay()
                {
                    Status = OperationCompletionStatus.NotFound,
                    AssetId = -1
                },
                _ => new AddAssetReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                }
            };
        }

        public override async Task<BidReplay> AddBid(BidRequest request, ServerCallContext context)
        {
            var response = await _bidService.Add(_mapper.Map<Domain.Input.Bid>(request), context.CancellationToken);
            
            return response.CreationStatus switch
            {
                CreationStatus.Success=>new BidReplay()
                {
                    Status = OperationCompletionStatus.Success,
                    SingleBid = _mapper.Map<SingleBidReplay>(response.Bid)
                },
                CreationStatus.BadRequest=> new BidReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                },
                _ => new BidReplay()
                {
                    Status = OperationCompletionStatus.NotFound
                }
            };
        }

        public override async Task<MultipleBidReplay> GetBids(SingleAssetsRequest request, ServerCallContext context)
        {
            var response = await _bidService.GetByAssetsById(request.Id, context.CancellationToken);

            return response.SearchStatus switch
            {
                SearchStatus.Success=>new MultipleBidReplay()
                {
                    Status = OperationCompletionStatus.Success,
                    SingleBids = { _mapper.Map<IEnumerable<AuctionAssetsServiceDefinition.SingleBidDetails>>(response.Bids) }
                },
                SearchStatus.BadRequest=>new MultipleBidReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                },
                _ => new MultipleBidReplay()
                {
                    Status = OperationCompletionStatus.NotFound
                }
            };
        }
    }
}
