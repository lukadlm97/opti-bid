using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAccountService
    {
        Task<OperationResult<UserResult>> GetDetails(string username, CancellationToken cancellationToken);
        Task<OperationResult<UserResult>> GetDetails(int userId, CancellationToken cancellationToken);
        Task<OperationResult<SingleUserResult>> Get(CancellationToken cancellationToken);
        Task<OperationResult<bool>> UpdateProfile(string username,Domain.Input.UserRequest userRequest, CancellationToken cancellationToken);
    }
}
