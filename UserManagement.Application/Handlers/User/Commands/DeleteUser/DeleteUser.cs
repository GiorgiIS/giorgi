using FluentValidation;
using MediatR;
using UserManagement.Application.Common;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Commands.DeleteUser
{
    public class DeleteUser : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
    }

    public class DeleteUserValidator : AbstractValidator<DeleteUser>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
