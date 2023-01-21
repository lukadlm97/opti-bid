using AutoMapper;
using Grpc.Core;
using MediatR;
using OptiBid.Microservices.Accounts.Grpc.UserServiceDefinition;
using OptiBid.Microservices.Accounts.Services.Command.Accounts;
using OptiBid.Microservices.Accounts.Services.Query.Accounts;

namespace OptiBid.Microservices.Accounts.Grpc.Services
{
    public class UserService:User.UserBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserService(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<UsersReplay> Get(UsersPagingRequest request, ServerCallContext context)
        {
            var users = _mapper.Map<IEnumerable<Domain.DTOs.User>, IEnumerable<UserServiceDefinition.SingleUser>>(
                await _mediator.Send(new GetAccountsCommand(), context.CancellationToken));

            return new UsersReplay()
            {
                Users = { users }
            };
        }

        public override async Task<UserDetailsReplay> GetById(UsersRequest request, ServerCallContext context)
        {
            var (notNull, user) = await _mediator.Send(new GetAccountByIdCommand() { Id = request.Id },
                cancellationToken: context.CancellationToken);

            if (notNull)
            {
                return new UserDetailsReplay()
                {
                    User = _mapper.Map<Domain.DTOs.UserDetails, UserServiceDefinition.SingleUserDetails>(user),
                    Status = OperationCompletionStatus.Success
                };
              
            }
            return new UserDetailsReplay()
            {
                Status = OperationCompletionStatus.NotFound
            };


        }


        public override async Task<RegisterReplay> Register(UserRegisterRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(
                new CreateAccountCommand()
                {
                    User = _mapper.Map<Domain.Entities.User>(request)
                }, context.CancellationToken);
            if (result == null)
            {
                return new RegisterReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                };
            }

            return new
                RegisterReplay()
                {
                    Status = OperationCompletionStatus.Success,
                    User = _mapper.Map<UserServiceDefinition.SingleUser>(result)
                };
        }


        public override async Task<UserDetailsReplay> SignIn(UserRequest request, ServerCallContext context)
        {
            var (notNull, user) =
                await _mediator.Send(
                    new CheckAccountCommand() { Username = request.Username, Password = request.Password },
                    context.CancellationToken);
            if (notNull)
            {
                return new UserDetailsReplay()
                {
                    Status = OperationCompletionStatus.Success,
                    User = _mapper.Map<SingleUserDetails>(user)
                };
                
            }
            return new UserDetailsReplay()
            {
                Status = OperationCompletionStatus.BadRequest
            };


        }

        public override async Task<RegisterReplay> UpdateProfile(UpdateRequest request, ServerCallContext context)
        {
            var result =
                await _mediator.Send(
                    new UpdateAccountCommand()
                        { UserId = request.UserId, User = _mapper.Map<Domain.Entities.User>(request.User) },
                    context.CancellationToken);

            if (result == null)
            {
                return new RegisterReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                };
            }

            return new
                RegisterReplay()
                {
                    Status = OperationCompletionStatus.Success,
                    User = _mapper.Map<SingleUser>(result)
                };
        }
    }
}
