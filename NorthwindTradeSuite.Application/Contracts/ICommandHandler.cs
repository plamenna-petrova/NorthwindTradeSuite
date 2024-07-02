using MediatR;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {

    }
}
