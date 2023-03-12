using Microsoft.AspNetCore.Mvc;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.Services;
using System.Security.Claims;
using OptiBid.API.Utilities;

namespace OptiBid.API.Controllers
{  /// <summary>
    /// Controller which return auction related stuff
    /// </summary>  [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            this._bidService = bidService;
        }

        /// <summary>
        /// Returns auction assets by request at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available auction assets
        ///
        ///     GET /assets
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<BidDetailsReply>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{assetId}")]
        public async Task<ActionResult<IEnumerable<BidDetailsReply>?>> GetAssets(int assetId, CancellationToken cancellationToken = default)
        {
            return await _bidService.Get(assetId, cancellationToken)
                .ToCollectionActionResult();
        }


        /// <summary>
        /// Returns auction assets by request at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available auction assets
        ///
        ///     GET /assets
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<BidDetailsReply>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{assetId}")]
        public async Task<ActionResult<BidReply>> Add(int assetId,[FromBody]BidRequest bidRequest, CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
                return await _bidService.Add(assetId, userName, bidRequest,cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }
    }
}
