using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Application.Handlers.User.Queries.GetUser;
using UserManagement.Common.EfCore.Contracts;
using UserManagement.Common.EfCore.Pagination;
using UserManagement.Common.Mediator.Attributes;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Queries.GetUsers
{
    [RequestLogging]
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PagedResult<GetUserDto>>>
    {
        private readonly ISqlRepository<Domain.User, int> _useSqlRepository;

        public GetUsersQueryHandler(ISqlRepository<Domain.User, int> useSqlRepository)
        {
            _useSqlRepository = useSqlRepository;
        }

        public async Task<Result<PagedResult<GetUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _useSqlRepository.BrowseAsync(x => !x.IsDeleted, request);

            if (pagedResult.IsEmpty)
            {
                return Result.NotFound<PagedResult<GetUserDto>>("Couldn't find entities with provided parameters");
            }

            var products = pagedResult.Items.Select(c => new GetUserDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName

            });

            return Result.Ok(value: PagedResult<GetUserDto>.From(pagedResult, products));
        }
    }
}
