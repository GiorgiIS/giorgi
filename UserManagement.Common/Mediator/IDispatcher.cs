using System.Threading.Tasks;
using MediatR;

namespace UserManagement.Common.Mediator
{
    public interface IDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IRequest<TResult> query);
        Task NotifyAsync<TEvent>(TEvent @event) where TEvent : INotification;
        Task<TResponse> RequestAsync<TResponse>(IRequest<TResponse> request);
        Task Request<TResponse>(IRequest request);
    }
}