﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Query.Profession;

namespace OptiBid.Microservices.Accounts.Web.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfessionsController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<ActionResult<List<Profession>>> Professions()
        {
            try
            {
                return await _mediator.Send(new GetProfessionsCommand());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
