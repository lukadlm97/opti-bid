using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Auction.Domain.Entities;
using OptiBid.Microservices.Auction.Persistence;

namespace OptiBid.Microservices.Auction.Data.Repositories
{
    public class AuctionAssetsRepository:Repository<AuctionAsset>,IAuctionAssetsRepository
    {
        private readonly AuctionContext _auctionContext;

        public AuctionAssetsRepository(AuctionContext auctionContext) : base(auctionContext)
        {
            _auctionContext = auctionContext;
        }

        public async Task<AuctionAsset> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await _auctionContext.AuctionAssets
                .Include(x => x.MediaUrls)
                .Include(x=>x.Bids).FirstAsync(x=>x.Id==id,cancellationToken);
        }

        public async Task Delete(AuctionAsset auctionAsset, CancellationToken cancellationToken = default)
        {
            _auctionContext.Remove(auctionAsset);
        }
        
    }
}
