using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Domain.Output.User;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<OperationResult<SignInResult>> SignIn(string username, string password,CancellationToken cancellationToken=default);
        Task<OperationResult<string>> Register(Domain.Input.UserRequest request,CancellationToken cancellationToken=default);
        Task<OperationResult<string>> RenewToken(string username,string refreshToken, CancellationToken cancellationToken = default);
        Task<OperationResult<SecondStepResult>> Verify(string username, string code, CancellationToken cancellationToken);
        Task<OperationResult<SecondStepResult>> Validate(string username, string code, CancellationToken cancellationToken);
    
    }
}
