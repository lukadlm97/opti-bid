using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAuctionAssetService
    {
        Task<OperationResult<Domain.Output.Auction.Asset>> GetAssets(PagingRequest pagingRequest,CancellationToken cancellationToken=default);
        Task<OperationResult<Domain.Output.Auction.Asset>> GetAssetById(int id, CancellationToken cancellationToken = default);

        Task<OperationResult<Domain.Output.Auction.UpsertResult>> Insert(Domain.Input.UpsertAssetRequest assetRequest,
            CancellationToken cancellationToken = default);
        Task<OperationResult<Domain.Output.Auction.UpsertResult>> Update(int id,UpsertAssetRequest assetRequest,CancellationToken cancellationToken = default);
        Task<OperationResult<Domain.Output.Auction.UpsertResult>> Delete(int id,  CancellationToken cancellationToken = default);

    }
}
