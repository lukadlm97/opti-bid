using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Command.Accounts;
using OptiBid.Microservices.Accounts.Services.Query.Accounts;

namespace OptiBid.Microservices.Accounts.Web.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Action to see all existing customers.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the customers were loaded</response>
        /// <response code="400">Returned if the customers couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.DTOs.User>>> Users()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAccountsCommand()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        /// <summary>
        /// Action to see all existing customers.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the customers were loaded</response>
        /// <response code="400">Returned if the customers couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Domain.DTOs.User>> Users([FromBody] Domain.Input.RegisterAccountModel registerAccountModel)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateAccountCommand()
                {
                    User = _mapper.Map<Domain.Entities.User>(registerAccountModel)
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        /// <summary>
        /// Action to see all existing customers.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the customers were loaded</response>
        /// <response code="400">Returned if the customers couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.DTOs.UserDetails>> Users(int id)
        {
            try
            {
                var (isNull,user) = await _mediator.Send(new GetAccountByIdCommand()
                {
                    Id = id
                });
                return isNull ? NotFound() : Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Action to see all existing customers.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the customers were loaded</response>
        /// <response code="400">Returned if the customers couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("/signIn")]
        public async Task<ActionResult<Domain.DTOs.UserDetails>> LogIn([FromBody]Domain.Input.SignInModel singInModel)
        {
            try
            {
                var (isSuccess, user) = await _mediator.Send(new CheckAccountCommand()
                {
                    Username = singInModel.Username,
                    Password = singInModel.Password
                });
                return isSuccess ? Ok(user): Unauthorized() ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to see all existing customers.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the customers were loaded</response>
        /// <response code="400">Returned if the customers couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Domain.DTOs.User>> UpdateUser(int id,[FromBody] Domain.Input.RegisterAccountModel registerAccountModel)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateAccountCommand()
                {
                    UserId = id,
                    User = _mapper.Map<Domain.Entities.User>(registerAccountModel)
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
