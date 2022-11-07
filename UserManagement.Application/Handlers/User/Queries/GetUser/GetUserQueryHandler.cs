using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Common.EfCore.Contracts;
using UserManagement.Common.Mediator.Attributes;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Queries.GetUser
{
    [RequestLogging]
    [RequestValidation]
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserDto>>
    {
        private readonly ISqlRepository<Domain.User, int> _userSqlRepository;

        public GetUserQueryHandler(ISqlRepository<Domain.User, int> userSqlRepository)
        {
            _userSqlRepository = userSqlRepository;
        }

        public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {


            throw new Exception("Something terrible happened in database");
            var user = await _userSqlRepository.GetAsync(request.Id.Value);

            if (user == null)
            {
                return Result.NotFound<GetUserDto>($"Couldn't find entity with provided identifier {request.Id.Value}");
            }

            var result = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return Result.Ok(value: result);
        }
    }
}
