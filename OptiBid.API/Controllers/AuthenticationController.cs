using Microsoft.AspNetCore.Mvc;
using OptiBid.API.Utilities;
using OptiBid.Microservices.Contracts.Services;
using System.Security.Claims;

namespace OptiBid.API.Controllers
{  /// <summary>
    /// Controller which hold enumerations data related to account service
    /// </summary>
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticationController:ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
        [HttpPost("signIn")]
        public async Task<ActionResult<Microservices.Contracts.Domain.Output.User.SignInResult>> SignIn(
            [FromBody] OptiBid.Microservices.Contracts.Domain.Input.SignInRequest userRequest,CancellationToken cancellationToken = default)
        {
            return await _authenticationService.SignIn(userRequest.Username,userRequest.Password, cancellationToken)
                .ToActionResult();
        }
        /// <summary>
        /// Allow user to register at system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents register user model (user details)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for register new user
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(
            [FromBody] OptiBid.Microservices.Contracts.Domain.Input.UserRequest userRequest, CancellationToken cancellationToken = default)
        {
            return await _authenticationService.Register(userRequest, cancellationToken)
                .ToActionResult();
        }

        /// <summary>
        /// Allow user to verify his account at system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents two fa request </param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for verify  user
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("verify")]
        public async Task<ActionResult<string>> Verify(
            [FromBody] OptiBid.Microservices.Contracts.Domain.Input.TwoFaRequest twoFaRequest, CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                return await _authenticationService.Verify(userName, twoFaRequest.Code, cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }
        /// <summary>
        /// Allow user to validate his account at system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents two fa request </param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for validate two fa  user
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("validate")]
        public async Task<ActionResult<string>> Validate(
            [FromBody] OptiBid.Microservices.Contracts.Domain.Input.TwoFaRequest twoFaRequest, CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                return await _authenticationService.Validate(userName, twoFaRequest.Code, cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }

        /// <summary>
        /// Allow user to refresh his existing token.
        /// </summary>
        ///
        /// <param name="userRequest"> represents two fa request </param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for refresh token
        ///
        ///     POST /refreshToken
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("refreshToken")]
        public async Task<ActionResult<string>> RefreshToken(
            [FromBody] OptiBid.Microservices.Contracts.Domain.Input.RefreshTokenRequest refreshToken, CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                return await _authenticationService.RenewToken(userName, refreshToken.RefreshToken, cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }


    }
}
