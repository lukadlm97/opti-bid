using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountAssetsQueryHandler : IRequestHandler<GetAccountAssetsCommand,(string,string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountAssetsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async 
            Task<(string, string)> Handle(GetAccountAssetsCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork._usersRepository.GetByUsername(request.Username, cancellationToken);
            (string, string) userAsset = await _unitOfWork._accountAssetsRepository.Get(user.ID, cancellationToken);

            return (userAsset.Item1, userAsset.Item2);

        }
    }
}
