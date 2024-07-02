using MediatR;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {

    }
}
