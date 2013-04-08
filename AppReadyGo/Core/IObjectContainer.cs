using AppReadyGo.Core.Commands;
using AppReadyGo.Core.Queries;

namespace AppReadyGo.Core
{
    public interface IObjectContainer
    {
        TResult RunQuery<TResult>(IQuery<TResult> query);

        CommandResult<TResult> Dispatch<TResult>(ICommand<TResult> command);

        CurrentUserDetails CurrentUserDetails { get; }

        void ClearCurrentUserDetails();
    }
}
