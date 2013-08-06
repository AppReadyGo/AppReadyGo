using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Commands.Users;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Core.Commands.Content;
using AppReadyGo.Domain.Model.Content;

namespace AppReadyGo.Domain.CommandHandlers.Content
{
    public class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand, int>
    {
        public int Execute(ISession session, UpdateItemCommand cmd)
        {
            var item = session.Get<Item>(cmd.Id);
            item.Update(cmd.Value);
            return item.Id;
        }
    }
}
