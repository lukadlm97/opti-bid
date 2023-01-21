using AutoMapper;
using Grpc.Core;
using MediatR;
using OptiBid.Microservices.Accounts.Grpc.DashboardServiceDefinition;
using OptiBid.Microservices.Accounts.Services.Query.ContactType;
using OptiBid.Microservices.Accounts.Services.Query.Country;
using OptiBid.Microservices.Accounts.Services.Query.Profession;
using OptiBid.Microservices.Accounts.Services.Query.UserRole;

namespace OptiBid.Microservices.Accounts.Grpc.Services
{
    public class DashboardService:Dashboard.DashboardBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DashboardService(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public override async Task<DashboardServiceDefinition.CountryReply> GetAllCountries(DashboardServiceDefinition.EmptyRequest request, ServerCallContext context)
        {
            var countries =
                _mapper.Map<IEnumerable<Domain.DTOs.Country>, IEnumerable<DashboardServiceDefinition.SingleCountry>>(
                    await _mediator.Send(new GetCountriesCommand(), context.CancellationToken));
            return new CountryReply()
            {
                Countries = { countries }
            };
        }

        public override async Task<ContactTypeReply> GetAllContactTypes(EmptyRequest request, ServerCallContext context)
        {
            var contactTypes = _mapper.Map<IEnumerable<Domain.DTOs.ContactType>, IEnumerable<DashboardServiceDefinition.SingleContactType>>(
                await _mediator.Send(new GetContactTypeCommand(), context.CancellationToken));

            return new ContactTypeReply()
            {
                ContactTypes = { contactTypes }
            };
        }

        public override async Task<ProfessionReply> GetAllProfessions(EmptyRequest request, ServerCallContext context)
        {
            var professions = _mapper.Map<IEnumerable<Domain.DTOs.Profession>, IEnumerable<DashboardServiceDefinition.SingleProfession>>(
                await _mediator.Send(new GetProfessionsCommand(), context.CancellationToken));

            return new ProfessionReply()
            {
                Professions = {  professions}
            
            };
        }

        public override async Task<UserRoleReply> GetAllUserRoles(EmptyRequest request, ServerCallContext context)
        {
            var userRoles = _mapper.Map<IEnumerable<Domain.DTOs.UserRoles>, IEnumerable<DashboardServiceDefinition.SingleUserRole>>(
                await _mediator.Send(new GetUserRolesCommand(), context.CancellationToken));

            return new UserRoleReply()
            {
                UserRoles = {  userRoles }
            };
        }
    }
}
