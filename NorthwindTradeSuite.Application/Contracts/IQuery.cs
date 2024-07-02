using MediatR;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {

    }
}
