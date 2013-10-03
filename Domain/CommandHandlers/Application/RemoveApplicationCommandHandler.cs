using System;
using System.Linq;
using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class RemoveApplicationCommandHandler : ICommandHandler<RemoveApplicationCommand, RemoveApplicationCommandResult>
    {
        public RemoveApplicationCommandResult Execute(ISession session, RemoveApplicationCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.Id);

            var result = new RemoveApplicationCommandResult
            {
                AppId = app.Id,
                IconExt = app.IconExt,
                Screens = app.Screens.Select(s => new Tuple<int,string>(s.Id, s.FileExtension)).ToArray(),
                Screenshots = app.Screenshots.Select(s => new Tuple<int, string>(s.Id, s.FileExtension)).ToArray()
            };
            foreach (var p in app.Publishes)
            {
                session.Delete(p);
            }
            foreach (var s in app.Screenshots)
            {
                session.Delete(s);
            }
            session.Flush();
            session.Delete(app);
            return result;
        }
    }
}
