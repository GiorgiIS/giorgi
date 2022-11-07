using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Common.EfCore;
using UserManagement.Common.EfCore.Contracts;
using UserManagement.Common.Mediator.Attributes;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Commands.CreateUser
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class CreateUserHandler : IRequestHandler<CreateUser, Result<int>>
    {
        private readonly ISqlRepository<Domain.User, int> _useSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(ISqlRepository<Domain.User, int> useSqlRepository, IUnitOfWork unitOfWork)
        {
            _useSqlRepository = useSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsDeleted = false
            };

            await _useSqlRepository.AddAsync(user);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Created, data: user.Id));
        }
    }
}
