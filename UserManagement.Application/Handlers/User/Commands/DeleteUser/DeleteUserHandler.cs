using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Common.EfCore.Contracts;
using UserManagement.Common.Mediator.Attributes;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Commands.DeleteUser
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteUserHandler : IRequestHandler<DeleteUser, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.User, int> _usersSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(ISqlRepository<Domain.User, int> usersSqlRepository, IUnitOfWork unitOfWork)
        {
            _usersSqlRepository = usersSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteUser request, CancellationToken cancellationToken)
        {

            var user = await _usersSqlRepository.GetAsync(request.Id.Value, new string[] { });
            if (user == null)
            {
                return Result.NotFound($"Entity wasn't found in database with provided identifier {request.Id}");
            }

            await _usersSqlRepository.DeleteAsync(user.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
