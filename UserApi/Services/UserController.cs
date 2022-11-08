using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using UserManagement.Application.Handlers.User.Commands.CreateUser;
using UserManagement.Application.Handlers.User.Commands.DeleteUser;
using UserManagement.Application.Handlers.User.Queries.GetUser;
using UserManagement.Application.Handlers.User.Queries.GetUsers;
using UserManagement.Common.EfCore.Pagination;
using UserManagement.Common.Mediator;
using UserManagement.Common.Mvc;
using UserManagement.Common.Swagger;

namespace UserManagement.Api.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ServiceController
    {
        public UserController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost]
        [SwaggerOperation("create  user")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of crated entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(401, "Operation couldn't be done, user is not authorized", typeof(SwaggerResultUnauthorized))]
        [SwaggerResponse(403, "Operation couldn't be done, user is does not have permission", typeof(SwaggerResulForbidden))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(422, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateUser command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation("delete user")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(401, "Operation couldn't be done, user is not authorized", typeof(SwaggerResultUnauthorized))]
        [SwaggerResponse(403, "Operation couldn't be done, user is does not have permission", typeof(SwaggerResulForbidden))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(422, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteUser command)
        {
            return await ExecuteAsync(command);
        }


        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve user from database")]
        [SwaggerResponse(200, "user with provided identifier", typeof(SwaggerResultGet<GetUserDto>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(401, "Operation couldn't be done, user is not authorized", typeof(SwaggerResultUnauthorized))]
        [SwaggerResponse(403, "Operation couldn't be done, user is does not have permission", typeof(SwaggerResulForbidden))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetUserDto>> Get([FromRoute] GetUserQuery query)
        {
            return await QueryAsync(query);
        }


        [HttpGet]
        [SwaggerOperation("retrieve users from database")]
        [SwaggerResponse(200, "The list of active users", typeof(SwaggerResultGet<PagedResult<GetUserDto>>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(401, "Operation couldn't be done, user is not authorized", typeof(SwaggerResultUnauthorized))]
        [SwaggerResponse(403, "Operation couldn't be done, user is does not have permission", typeof(SwaggerResulForbidden))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<PagedResult<GetUserDto>>> Get([FromQuery] GetUsersQuery query)
        {
            return await QueryAsync<Result<PagedResult<GetUserDto>>>(query);
        }

    }
}
