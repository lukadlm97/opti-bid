using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Services;
using System.Security.Claims;
using OptiBid.API.Utilities;
using OptiBid.Microservices.Contracts.Domain.Input;

namespace OptiBid.API.Controllers
{ 
    /// <summary>
    /// Controller which hold enumerations data related to account service
    /// </summary>
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("register")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Microservices.Contracts.Domain.Output.Customer.CustomerResult>> Get(CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
                return await _customerService.OpenAccount(new CustomerRequest(-1, userName), cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }

        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Microservices.Contracts.Domain.Output.Customer.CustomerDetailsResult>> Get(int id,CancellationToken cancellationToken = default)
        {

            return await _customerService.Get(id, cancellationToken).ToActionResult();
            
        }

        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Microservices.Contracts.Domain.Output.Customer.CustomerDetailsResult>>> GetCollection(CancellationToken cancellationToken = default)
        {

            return await _customerService.Get(new PagingRequest(),cancellationToken).ToCollectionActionResult();

        }
    }
}
