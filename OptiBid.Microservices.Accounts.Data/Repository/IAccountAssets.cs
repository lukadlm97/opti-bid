using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Data.Repository
{
    public interface IAccountAssets
    {
        Task AddTwoFAKey(UserTwoFAAssets userTwoFaAssets,CancellationToken cancellationToken=default);
        Task AddRefreshToken(UserToken userToken, CancellationToken cancellationToken=default);
        Task<(string,string)> Get(int userId,CancellationToken cancellationToken=default);
    }
}
