using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IBidService
    {
        Task<OperationResult<Domain.Output.Auction.BidReply>> Add(int assetId,string username,Domain.Input.BidRequest bidRequest,CancellationToken cancellationToken=default);
        Task<OperationResult<Domain.Output.Auction.BidDetailsReply>> Get(int assetId,CancellationToken cancellationToken=default);
    }
}
