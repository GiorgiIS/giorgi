using FluentValidation;
using MediatR;
using UserManagement.Application.Common;
using UserManagement.Common.Mvc;

namespace UserManagement.Application.Handlers.User.Queries.GetUser
{
    public class GetUserQuery : IRequest<Result<GetUserDto>>
    {
        public int? Id { get; set; }
    }

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
