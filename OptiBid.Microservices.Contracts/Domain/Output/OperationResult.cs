using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Output
{
    public record OperationResult<T>(
         T? Data ,
         IEnumerable<T>? Collection ,
         OperationResultStatus? Status,
         string? ErrorMessage
    );
    public enum OperationResultStatus
    {
        Success,
        BadRequest,
        NotFound,
        Forbidden,
        Error
    }
}
