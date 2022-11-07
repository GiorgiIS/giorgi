using UserManagement.Common.Mediator.Decorators;

namespace UserManagement.Common.Mediator.Attributes
{
    public class RequestTransactionAttribute : BaseDecoratorAttribute
    {
        public RequestTransactionAttribute() : base(typeof(RequestTransactionDecorator<,>))
        { }
    }
}