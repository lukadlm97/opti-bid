using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Accounts.Grpc.UserServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;
using UserRequest = OptiBid.Microservices.Contracts.Domain.Input.UserRequest;

namespace OptiBid.Microservices.Services.GrpcServices
{
    public class UserGrpcService:IAccountGrpcService
    {
        private readonly ILogger<UserGrpcService> _logger;
        private readonly IAccountGrpcFactory _accountGrpcFactory;

        public UserGrpcService(ILogger<UserGrpcService> logger, IAccountGrpcFactory accountGrpcFactory)
        {
            _logger = logger;
            _accountGrpcFactory = accountGrpcFactory;
        }
        public async Task<UserResult> SignIn(string username, string password,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _accountGrpcFactory.GetUserClient();
                var reply = await grpcClient.SignInAsync(new Accounts.Grpc.UserServiceDefinition.UserRequest()
                {
                    Username = username,
                    Password = password
                });

               
                return reply.Status switch
                {
                    OperationCompletionStatus.Success => new UserResult()
                    {
                        Id = reply.User.Id,
                        FirstLogIn = reply.User.FirstLogIn,
                        Username = reply.User.Username,
                        UserRoleID = reply.User.UserRoleID
                    },
                    OperationCompletionStatus.BadRequest=>new UserResult()
                    {
                        Id=-1
                    },
                    OperationCompletionStatus.NotFound=>new UserResult()
                    {
                        Id =0
                    },
                    _=>null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<AssetsResult> GetAssets(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                var client =  _accountGrpcFactory.GetUserClient();
                var replay = await client.GetUserAssetsAsync(new Accounts.Grpc.UserServiceDefinition.UserRequest()
                {
                    Username = username
                },cancellationToken:cancellationToken);
                if (replay != null)
                {
                    return replay.Status switch
                    {
                        OperationCompletionStatus.Success => new AssetsResult()
                        {
                            RefreshToken = replay.Response.Token,
                            TwoFaSource = replay.Response.TwoFa
                        },
                        OperationCompletionStatus.BadRequest or OperationCompletionStatus.NotFound => default
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return default;
        }

        public async Task<AssetsResult> RefreshToken(string username, string refreshToken,CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _accountGrpcFactory.GetUserClient();
                var replay = await client.RefreshTokenAsync(new RefreshTokenRequest()
                {
                    Username = username,
                    RefreshToken = refreshToken
                }, cancellationToken: cancellationToken);

                if (replay != null)
                {
                    return replay.Status switch
                    {
                        OperationCompletionStatus.Success => new AssetsResult()
                        {
                            RefreshToken = replay.GeneratedRefreshToken
                        },
                        OperationCompletionStatus.BadRequest or OperationCompletionStatus.NotFound => default,
                        _ => default
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return default;
        }

        public async Task<bool> ConfirmFirstSignIn(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _accountGrpcFactory.GetUserClient();
                var replay = await client.ConfirmedFirstLogInAsync(new Accounts.Grpc.UserServiceDefinition.UserRequest()
                {
                    Username = username
                }, cancellationToken: cancellationToken);

                if (replay != null)
                {
                    return replay.Status switch
                    {
                        OperationCompletionStatus.Success => true,
                        OperationCompletionStatus.BadRequest or OperationCompletionStatus.NotFound=>false,
                        _ => false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<UserResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _accountGrpcFactory.GetUserClient();
                var replay = await client.GetByIdAsync(new UsersRequest()
                {
                    Id = id
                }, cancellationToken: cancellationToken);

                if (replay != null)
                {
                    if (replay.Status == OperationCompletionStatus.Success)
                    {
                        var skills = new List<Skill>();
                        foreach (var singleSkill in replay.User.Skills)
                        {
                            var skill = new Skill()
                            {
                                Id = singleSkill.Id,
                                IsActive = singleSkill.IsActive,
                                ProfessionId = singleSkill.ProfessionId
                            };
                            skills.Add(skill);
                        }

                        var contacts = new List<Contact>();
                        foreach (var singleContact in replay.User.Contacts)
                        {
                            var contact = new Contact()
                            {
                                ContactTypeId = singleContact.ContactTypeId,
                                Content = singleContact.Content,
                                Id = singleContact.Id,
                                IsActive = singleContact.IsActive
                            };
                            contacts.Add(contact);
                        }

                        return new UserResult()
                        {
                            Id = replay.User.Id,
                            CountryId = replay.User.CountryId,
                            FirstLogIn = replay.User.FirstLogIn,
                            Name = replay.User.Name,
                            UserRoleID = replay.User.UserRoleID,
                            Username = replay.User.Username,
                            Contacts = contacts,
                            Skills = skills
                        };
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return default;
        }

        public async Task<UserResult> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _accountGrpcFactory.GetUserClient();
                var replay = await client.GetByUsernameAsync(new Accounts.Grpc.UserServiceDefinition.UserRequest()
                {
                    Username = username
                }, cancellationToken: cancellationToken);

                if (replay != null)
                {
                    if (replay.Status == OperationCompletionStatus.Success)
                    {
                        var skills = new List<Skill>();
                        foreach (var singleSkill in replay.User.Skills)
                        {
                            var skill = new Skill()
                            {
                                Id = singleSkill.Id,
                                IsActive = singleSkill.IsActive,
                                ProfessionId = singleSkill.ProfessionId
                            };
                            skills.Add(skill);
                        }

                        var contacts = new List<Contact>();
                        foreach (var singleContact in replay.User.Contacts)
                        {
                            var contact = new Contact()
                            {
                                ContactTypeId = singleContact.ContactTypeId,
                                Content = singleContact.Content,
                                Id = singleContact.Id,
                                IsActive = singleContact.IsActive
                            };
                            contacts.Add(contact);
                        }

                        return new UserResult()
                        {
                            Id = replay.User.Id,
                            CountryId = replay.User.CountryId,
                            FirstLogIn = replay.User.FirstLogIn,
                            Name = replay.User.Name,
                            UserRoleID = replay.User.UserRoleID,
                            Username = replay.User.Username,
                            Contacts = contacts,
                            Skills = skills
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return default;
        }

        public async Task<bool> Register(UserRequest userRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var skillRequests = new List<SkillRequest>();
                foreach (var userRequestSkill in userRequest.Skills)
                {
                    var singleSkill = new SkillRequest()
                    {
                        ProfessionId = userRequestSkill.ProfessionId,
                    };
                    skillRequests.Add(singleSkill);
                }

                var contacts = new List<ContactRequest>();
                foreach (var userRequestContact in userRequest.Contacts)
                {
                    var contact = new ContactRequest()
                    {
                        Content = userRequestContact.Content,
                        ContactTypeId = userRequestContact.ContactTypeId
                    };
                    contacts.Add(contact);
                }

                var request = new UserRegisterRequest()
                {
                    Email = userRequest.Email,
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    Password = userRequest.Password,
                    CountryId = userRequest.CountryId,
                    Contacts = { contacts },
                    Skills = { skillRequests }
                };
                var client = _accountGrpcFactory.GetUserClient();
                var replay = await client.RegisterAsync(request, cancellationToken: cancellationToken);
                if (replay != null)
                {
                    return replay.Status switch
                    {
                        OperationCompletionStatus.Success => true,
                        OperationCompletionStatus.BadRequest or OperationCompletionStatus.NotFound => false,
                        _ => false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }
    }
}
