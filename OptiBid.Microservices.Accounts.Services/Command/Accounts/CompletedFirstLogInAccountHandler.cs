using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CompletedFirstLogInAccountHandler : IRequestHandler<CompletedFirstLogInAccountCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompletedFirstLogInAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CompletedFirstLogInAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork._usersRepository.GetByUsername(request.Username, cancellationToken);
            if (user == null)
            {
                return false;
            }

            user.FirstLogIn = false;
            _unitOfWork._usersRepository.UpdateUser(user);
            await _unitOfWork.Commit(cancellationToken);
            return true;
        }
    }
}
