using UserManagement.Common.Mediator.Decorators;

namespace UserManagement.Common.Mediator.Attributes
{
    public class RequestValidationAttribute : BaseDecoratorAttribute
    {
        public RequestValidationAttribute() : base(typeof(RequestValidationDecorator<,>))
        { }
    }
}