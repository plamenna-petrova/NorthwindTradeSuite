using MediatR;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {

    }

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {

    }
}
