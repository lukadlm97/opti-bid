using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.API.Utilities
{
    public static class OperationResultExtensions
    {
        public static async Task<ActionResult<T>> ToActionResult<T>(this Task<OperationResult<T>> operation)
        {
            var result = await operation;

            return result.Status switch
            {
                OperationResultStatus.Success => new OkObjectResult(result.Data),
                OperationResultStatus.BadRequest => new BadRequestResult(),
                OperationResultStatus.NotFound => new NotFoundResult(),
                OperationResultStatus.Forbidden => new ForbidResult(),
                _ => new ObjectResult("Internal server error") { StatusCode = StatusCodes.Status500InternalServerError },
            };
        }
        public static async Task<ActionResult<IEnumerable<T>?>> ToCollectionActionResult<T>(this Task<OperationResult<T>> operation)
        {
            var result = await operation;

            return result.Status switch
            {
                OperationResultStatus.Success => new OkObjectResult(result.Collection),
                OperationResultStatus.BadRequest => new BadRequestResult(),
                OperationResultStatus.NotFound => new NotFoundResult(),
                OperationResultStatus.Forbidden => new ForbidResult(),
                _ => new ObjectResult("Internal server error") { StatusCode = StatusCodes.Status500InternalServerError },
            };
        }
    }
}
