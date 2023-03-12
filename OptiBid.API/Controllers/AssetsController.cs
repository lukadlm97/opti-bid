using Microsoft.AspNetCore.Mvc;
using OptiBid.API.Utilities;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.Services;

namespace OptiBid.API.Controllers
{
    /// <summary>
    /// Controller which return auction related stuff
    /// </summary>  [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAuctionAssetService _auctionAssetService;

        public AssetsController(IAuctionAssetService auctionAssetService)
        {
            _auctionAssetService = auctionAssetService;
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
        [ProducesResponseType(typeof(IEnumerable<Asset>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Asset>?>> GetAssets([FromQuery]PagingRequest pagingRequest, CancellationToken cancellationToken = default)
        {
            return await _auctionAssetService.GetAssets(pagingRequest,cancellationToken)
                .ToCollectionActionResult();
        }

        /// <summary>
        /// Returns auction assets by id at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available auction assets
        ///
        ///     GET /assets/{id}
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id, CancellationToken cancellationToken = default)
        {
            return await _auctionAssetService.GetAssetById(id,cancellationToken)
                .ToActionResult();
        }

        /// <summary>
        /// Returns auction assets by id at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available auction assets
        ///
        ///     GET /assets/{id}
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("")]
        public async Task<ActionResult<UpsertResult>> Insert([FromBody]UpsertAssetRequest assetRequest, CancellationToken cancellationToken = default)
        {
            return await _auctionAssetService.Insert(assetRequest, cancellationToken)
                .ToActionResult();
        }


        /// <summary>
        /// Returns auction assets by id at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available auction assets
        ///
        ///     GET /assets/{id}
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<UpsertResult>> Update(int id,[FromBody] UpsertAssetRequest assetRequest, CancellationToken cancellationToken = default)
        {
            return await _auctionAssetService.Update(id,assetRequest, cancellationToken)
                .ToActionResult();
        }


        /// <summary>
        /// Returns auction assets by id at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available auction assets
        ///
        ///     GET /assets/{id}
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UpsertResult>> Delete(int id,  CancellationToken cancellationToken = default)
        {
            return await _auctionAssetService.Delete(id, cancellationToken)
                .ToActionResult();
        }



    }
}
