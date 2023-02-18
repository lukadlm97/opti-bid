using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.GrpcServices
{
    public interface IAccountGrpcService
    {
        Task<Domain.Output.User.UserResult> SignIn(string username,string password,CancellationToken cancellationToken=default);
        Task<Domain.Output.User.AssetsResult> GetAssets(string username,CancellationToken cancellationToken=default); 
        Task<Domain.Output.User.AssetsResult> RefreshToken(string username,string refreshToken, CancellationToken cancellationToken = default);
        Task<bool> ConfirmFirstSignIn(string username,CancellationToken cancellationToken=default);
        Task<Domain.Output.User.UserResult> GetById(int id,CancellationToken cancellationToken=default);
        Task<Domain.Output.User.UserResult> GetByUsername(string username, CancellationToken cancellationToken = default);
        Task<bool> Register(Domain.Input.UserRequest userRequest,CancellationToken cancellationToken=default);
        Task<IEnumerable<Domain.Output.User.SingleUserResult>> Get(CancellationToken cancellationToken=default);
        Task<bool> Update(int userId,Domain.Input.UserRequest userRequest, CancellationToken cancellationToken = default);
    }
}
