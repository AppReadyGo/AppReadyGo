using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.Model;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, int>
    {
        public int Execute(ISession session, CreateMemberCommand cmd)
        {
            var user = new Member(cmd.Email, cmd.Password);
            session.Save(user);
            return user.Id;
        }
    }

    public class CreateStaffCommandHandler : ICommandHandler<CreateStaffCommand, int>
    {
        public int Execute(ISession session, CreateStaffCommand cmd)
        {
            var user = new Staff(cmd.Email, cmd.Password);
            session.Save(user);
            return user.Id;
        }
    }

    public class CreateAPIMemberCommandHandler : ICommandHandler<CreateAPIMemberCommand, int>
    {
        public int Execute(ISession session, CreateAPIMemberCommand cmd)
        {
            var country = session.Get<Country>(cmd.CountryId);
            var appTypes = cmd.ApplicationTypes != null ? cmd.ApplicationTypes.Select(x => session.Get<ApplicationType>(x)).ToArray() : null;
            var user = new ApiMember(cmd.Email, cmd.Password, cmd.FirstName, cmd.LastName, cmd.Gender, cmd.AgeRange, country, cmd.Zip, appTypes);
            session.Save(user);
            return user.Id;
        }
    }
}
