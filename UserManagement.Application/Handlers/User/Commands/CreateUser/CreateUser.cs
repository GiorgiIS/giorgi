using FluentValidation;
using MediatR;
using UserManagement.Application.Common;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Commands.CreateUser
{
    public class CreateUser : IRequest<Result<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
