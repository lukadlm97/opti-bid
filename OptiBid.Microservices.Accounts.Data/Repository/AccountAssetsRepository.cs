using OptiBid.Microservices.Accounts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Domain.DTOs;

namespace OptiBid.Microservices.Accounts.Data.Repository
{
    public class AccountAssetsRepository:IAccountAssets
    {
        private readonly AccountsContext accountsContext;

        public AccountAssetsRepository(AccountsContext accountsContext)
        {
            this.accountsContext = accountsContext;
        }

        public async Task AddTwoFAKey(UserTwoFAAssets userTwoFaAssets, CancellationToken cancellationToken = default)
        {
            await this.accountsContext.UserTwoFaAssets.AddAsync(userTwoFaAssets, cancellationToken);
        }

        public async Task AddRefreshToken(UserToken userToken, CancellationToken cancellationToken = default)
        {
            await this.accountsContext.UserTokens.AddAsync(userToken, cancellationToken);
        }

        public async Task InvalidateTokens(int userId, CancellationToken cancellationToken = default)
        {
            var tokens = accountsContext.UserTokens.Where(x => x.UserID == userId);
            foreach (var userToken in tokens)
            {
                userToken.IsValid = false;
            }
            this.accountsContext.UpdateRange(tokens);
        }

        public async Task<(string, string)> Get(int userId, CancellationToken cancellationToken = default)
        {
            var userToken =
                await accountsContext.UserTokens.OrderBy(x=>x.ID).LastOrDefaultAsync(x => x.UserID == userId, cancellationToken);
            var userTwoFaAsset =
                await accountsContext.UserTwoFaAssets.OrderBy(x => x.ID).LastOrDefaultAsync(x => x.UserID == userId, cancellationToken);


            return (userToken.RefreshToken, userTwoFaAsset.Source);
        }
    }
}
