using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;

namespace OptiBid.Microservices.Accounts.Services.Query.Profession
{
    public class GetProfessionsQueryHandler:IRequestHandler<GetProfessionsCommand,List<Domain.Entities.Profession>>
    {
        private readonly IRepository<Domain.Entities.Profession> professionRepository;

        public GetProfessionsQueryHandler(IRepository<Domain.Entities.Profession> professionRepository)
        {
            this.professionRepository = professionRepository;
        }
        public async Task<List<Domain.Entities.Profession>> Handle(GetProfessionsCommand request, CancellationToken cancellationToken)
        {
            return this.professionRepository.GetAll().ToList();
        }
    }
}
