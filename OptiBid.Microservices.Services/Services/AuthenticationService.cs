﻿using Google.Authenticator;
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
        private readonly IJwtManager _jwtManager;
        private readonly ICategoryDashboardService _categoryDashboardService;

        public AuthenticationService(IAccountGrpcService accountGrpcService,IJwtManager jwtManager,ICategoryDashboardService categoryDashboardService)
        {
            _accountGrpcService = accountGrpcService;
            _jwtManager = jwtManager;
            _categoryDashboardService = categoryDashboardService;
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

            var signleRole =
                (await _categoryDashboardService.GetUserRoles(cancellationToken)).Collection.FirstOrDefault(x =>
                    x.ID == userResult.UserRoleID);
            if (signleRole == null)
            {
                return new OperationResult<SignInResult>()
                {
                    Status = OperationResultStatus.NotFound
                };

            }

            var token = _jwtManager.GenerateToken(userResult.Username, signleRole.Name);
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
                            RefreshToken = userAssets.RefreshToken,
                            Token = token
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
                        RefreshToken = userAssets.RefreshToken,
                        Token = token
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
        
        public async Task<OperationResult<SecondStepResult>> Verify(string username, string code, CancellationToken cancellationToken)
        {
            var userAssets = await _accountGrpcService.GetAssets(username, cancellationToken);

            if (userAssets != null)
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                bool result = tfa.ValidateTwoFactorPIN(userAssets.TwoFaSource, code);
                if (result)
                {
                    var isConfirmFirstSignIn = await _accountGrpcService.ConfirmFirstSignIn(username, cancellationToken);

                    var userResult = await _accountGrpcService.GetByUsername(username, cancellationToken);
                    var userRole =
                        (await _categoryDashboardService.GetUserRoles(cancellationToken)).Collection.FirstOrDefault(x =>
                            x.ID == userResult.UserRoleID);
                    if (!isConfirmFirstSignIn && userResult == null || userRole == null)
                    {
                        return new OperationResult<SecondStepResult>()
                        {
                            Status = OperationResultStatus.NotFound
                        };
                    }

                    var token = _jwtManager.GenerateToken(userResult.Username, userRole.Name);
                    return new OperationResult<SecondStepResult>()
                    {
                        Status = OperationResultStatus.Success,
                        Data = new SecondStepResult()
                        {
                            Token = token

                        }
                    };

                }
                return new OperationResult<SecondStepResult>()
                {
                    Status = OperationResultStatus.BadRequest,
                    ErrorMessage = "invalid 2fa code"
                };
            }

            return new OperationResult<SecondStepResult>()
            {
                Status = OperationResultStatus.NotFound
            };
        }

        public async Task<OperationResult<SecondStepResult>> Validate(string username, string code, CancellationToken cancellationToken)
        {
            var userAssets = await _accountGrpcService.GetAssets(username, cancellationToken);

            if (userAssets != null)
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                bool result = tfa.ValidateTwoFactorPIN(userAssets.TwoFaSource, code);
                if (result)
                {
                    var userResult = await _accountGrpcService.GetByUsername(username, cancellationToken);
                    var userRole =
                        (await _categoryDashboardService.GetUserRoles(cancellationToken)).Collection.FirstOrDefault(x =>
                            x.ID == userResult.UserRoleID);
                    if (userResult == null || userRole ==null)
                    {
                        return new OperationResult<SecondStepResult>()
                        {
                            Status = OperationResultStatus.NotFound
                        };
                    }

                    var token = _jwtManager.GenerateToken(userResult.Username, userRole.Name);
                    return new OperationResult<SecondStepResult>()
                    {
                        Status = OperationResultStatus.Success,
                        Data = new SecondStepResult()
                        {
                            Token = token
                        }
                    };

                }
                return new OperationResult<SecondStepResult>()
                {
                    Status = OperationResultStatus.BadRequest,
                    ErrorMessage = "invalid 2fa code"
                };
            }

            return new OperationResult<SecondStepResult>()
            {
                Status = OperationResultStatus.NotFound
            };
        }

        public async Task<OperationResult<UserResult>> GetDetails(string username, CancellationToken cancellationToken)
        {
            var userProfile = await _accountGrpcService.GetByUsername(username, cancellationToken);
            if (userProfile != null)
            {
                return new OperationResult<UserResult>()
                {
                    Data = userProfile,
                    Status = OperationResultStatus.Success
                };
            }

            return new OperationResult<UserResult>()
            {
                Status = OperationResultStatus.NotFound
            };
        }
    }
}
