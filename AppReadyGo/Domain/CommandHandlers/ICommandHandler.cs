using NHibernate;

namespace AppReadyGo.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand, TResult>
    {
        TResult Execute(ISession session, TCommand cmd);
    }
}
