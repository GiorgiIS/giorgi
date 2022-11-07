using UserManagement.Common.Mediator.Decorators;

namespace UserManagement.Common.Mediator.Attributes
{
    public class RequestLoggingAttribute : BaseDecoratorAttribute
    {
        public RequestLoggingAttribute() : base(typeof(RequestLoggingDecorator<,>))
        { }
    }
}