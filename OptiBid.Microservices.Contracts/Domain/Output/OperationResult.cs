using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Domain.Output
{
    public class OperationResult<T>
    {
        public T? Data { init; get; }
        public IEnumerable<T>? Collection { init;get; }
        public OperationResultStatus? Status { init; get; }
        public string? ErrorMessage { get; set; }

        public bool Success => Status == OperationResultStatus.Success;

    }
    public enum OperationResultStatus
    {
        Success,
        BadRequest,
        NotFound,
        Forbidden,
        Error
    }
}
