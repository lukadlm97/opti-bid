using Google.Authenticator;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;

namespace OptiBid.Microservices.Services.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IAccountGrpcService _accountGrpcService;

        public AuthenticationService(IAccountGrpcService accountGrpcService)
        {
            _accountGrpcService = accountGrpcService;
        }
        public async Task<OperationResult<SignInResult>> SignIn(string username, string password, CancellationToken cancellationToken = default)
        {
            var userResult = await _accountGrpcService.SignIn(username, password, cancellationToken);
            if (userResult == null)
            {
                return new OperationResult<SignInResult>()
                {
                    Status = OperationResultStatus.BadRequest
                };
            }
            if (userResult.Id == -1)
            {
                return new OperationResult<SignInResult>()
                {
                    Status = OperationResultStatus.BadRequest
                };
            }

            if (userResult.Id==0)
            {
                return new OperationResult<SignInResult>()
                {
                    Status = OperationResultStatus.NotFound
                };
            }

            if (userResult.FirstLogIn)
            {
                var userAssets = await _accountGrpcService.GetAssets(username, cancellationToken);
                if (userAssets != null)
                {
                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    SetupCode setupInfo = tfa.GenerateSetupCode("OptBid", userResult.Username, userAssets.TwoFaSource, false, 3);

                    string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                    string manualEntrySetupCode = setupInfo.ManualEntryKey;

                    return new OperationResult<SignInResult>()
                    {
                        Status = OperationResultStatus.Success,
                        Data = new SignInResult()
                        {
                            TwoFa = new TwoFaResult()
                            {
                                ManualEntryKey = manualEntrySetupCode,
                                QrCode = qrCodeImageUrl
                            },
                            Username = userResult.Username,
                            RefreshToken = userAssets.RefreshToken
                        },
                    };
                }
                else
                {
                    return new OperationResult<SignInResult>()
                    {
                        Status = OperationResultStatus.NotFound
                    };
                }
                
            }

            if (userResult.Id > 0)
            {

                var userAssets = await _accountGrpcService.GetAssets(username, cancellationToken);
                return new OperationResult<SignInResult>()
                {
                    Status = OperationResultStatus.Success,
                    Data = new SignInResult()
                    {
                        Username = userResult.Username,
                        RefreshToken = userAssets.RefreshToken
                    }
                };
            }


            return new OperationResult<SignInResult>()
            {
                Status = OperationResultStatus.BadRequest
            };
        }

        public async Task<OperationResult<string>> Register(UserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _accountGrpcService.Register(request, cancellationToken);
            if (result == true)
            {
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.Success,
                    Data = "successfully created account"
                };
            }

            return new OperationResult<string>()
            {
                Status = OperationResultStatus.BadRequest
            };
        }

        public async Task<OperationResult<string>> RenewToken(string username, string refreshToken, CancellationToken cancellationToken = default)
        {
            var result = await _accountGrpcService.RefreshToken(username, refreshToken,cancellationToken);
            if (result != default)
            {
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.Success,
                    Data = result.RefreshToken
                };
            }
            return new OperationResult<string>()
            {
                Status = OperationResultStatus.BadRequest
            };
        }

        public async Task<OperationResult<string>> Verify(string username, string code, CancellationToken cancellationToken)
        {
            var userAssets = await _accountGrpcService.GetAssets(username, cancellationToken);

            if (userAssets != null)
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                bool result = tfa.ValidateTwoFactorPIN(userAssets.TwoFaSource, code);
                if (result)
                {
                    var isConfirmFirstSignIn = await _accountGrpcService.ConfirmFirstSignIn(username, cancellationToken);

                    if (isConfirmFirstSignIn)
                    {
                        return new OperationResult<string>()
                        {
                            Status = OperationResultStatus.Success,

                        };
                    }

                    return new OperationResult<string>()
                    {
                        Status = OperationResultStatus.Error
                    };

                }
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.BadRequest,
                    ErrorMessage = "invalid 2fa code"
                };
            }

            return new OperationResult<string>()
            {
                Status = OperationResultStatus.NotFound
            };
        }

        public async Task<OperationResult<string>> Validate(string username, string code, CancellationToken cancellationToken)
        {
            var userAssets = await _accountGrpcService.GetAssets(username, cancellationToken);

            if (userAssets != null)
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                bool result = tfa.ValidateTwoFactorPIN(userAssets.TwoFaSource, code);
                if (result)
                {

                    return new OperationResult<string>()
                    {
                        Status = OperationResultStatus.Success
                    };

                }
                return new OperationResult<string>()
                {
                    Status = OperationResultStatus.Error,
                    ErrorMessage = "invalid 2fa code"
                };
            }

            return new OperationResult<string>()
            {
                Status = OperationResultStatus.NotFound
            };
        }
    }
}
