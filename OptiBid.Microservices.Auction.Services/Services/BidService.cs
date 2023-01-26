
using AutoMapper;
using OptiBid.Microservices.Auction.Messaging.Sender.Models;
using OptiBid.Microservices.Auction.Services.Enumerations;
using OptiBid.Microservices.Auction.Services.Models;
using OptiBid.Microservices.Auction.Services.UnitOfWork;
using OptiBid.Microservices.Auction.Services.Utilities;

namespace OptiBid.Microservices.Auction.Services.Services
{
    internal class BidService:IBidService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFireForgetHandler _fireForgetHandler;

        public BidService(IUnitOfWork unitOfWork,IMapper mapper,IFireForgetHandler fireForgetHandler)
        {
            this._unitOfWork=unitOfWork;
            _mapper=mapper;
            _fireForgetHandler=fireForgetHandler;
        }

        public async Task<BidResponse> Add(Domain.Input.Bid bid, CancellationToken cancellationToken = default)
        {
            try
            {
                var mappedBid = _mapper.Map<Domain.Entities.Bid>(bid);
                var asset = await _unitOfWork._auctionAssetsRepository.GetById(bid.AuctionAssetId, cancellationToken);
                var customer =
                    await _unitOfWork._customerRepository.FindByUsername(bid.CustomerUsername, cancellationToken);
                if (mappedBid == null || asset == null|| customer==null)
                {
                    return new BidResponse()
                    {
                        CreationStatus = CreationStatus.BadRequest
                    };
                }
                mappedBid.Customer=customer;
                asset.Bids.Add(mappedBid);
                await _unitOfWork._auctionAssetsRepository.Update(asset);
                await _unitOfWork.Commit(cancellationToken);
             
                _fireForgetHandler.Execute(x=>x.Send(new BidMessage()
                {
                    AssetId = asset.Id,
                    BidPrice = mappedBid.BidPrice,
                    AssetName = asset.Name,
                    BidDateTime = mappedBid.BidDate,
                    Bider = bid.CustomerUsername
                }));

                return new BidResponse()
                {
                    CreationStatus = CreationStatus.Success,
                    Bid = _mapper.Map<Domain.DTOs.Bid>(mappedBid)
                };
            }
            catch (Exception ex)
            {
                return new BidResponse()
                {
                    CreationStatus = CreationStatus.Error
                };
            }
           


        }

        public async Task<BidResponse> GetByAssetsById(int assetId, CancellationToken cancellationToken = default)
        {
            try
            {
                var asset = await _unitOfWork._auctionAssetsRepository.GetById(assetId, cancellationToken);
                if (asset == null)
                {
                    return new BidResponse()
                    {
                        SearchStatus = SearchStatus.NotFound
                    };
                }

                return new BidResponse()
                {
                    SearchStatus = SearchStatus.Success,
                    Bids = _mapper.Map<IEnumerable<Domain.DTOs.Bid>>(asset.Bids)
                };
            }
            catch (Exception ex)
            {
                return new BidResponse()
                {
                    SearchStatus = SearchStatus.BadRequest
                };
            }
        }
    }
}
