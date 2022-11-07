using MediatR;
using UserManagement.Application.Handlers.User.Queries.GetUser;
using UserManagement.Common.EfCore.Pagination;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Queries.GetUsers
{
    public class GetUsersQuery : PagedQueryBase, IRequest<Result<PagedResult<GetUserDto>>>
    {
    }
}
