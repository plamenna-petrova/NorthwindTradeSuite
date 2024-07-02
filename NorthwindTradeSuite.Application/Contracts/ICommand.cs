using MediatR;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface ICommand : IRequest
    {

    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}
