using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.Model;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class CreateThirdPartyAPIMemberCommandHandler : ICommandHandler<CreateThirdPartyAPIMemberCommand, CreateThirdPartyAPIMemberCommand.Result>
    {
        public CreateThirdPartyAPIMemberCommand.Result Execute(ISession session, CreateThirdPartyAPIMemberCommand cmd)
        {
            var country = session.Get<Country>(cmd.CountryId);
            var appTypes = cmd.ApplicationTypes != null ? cmd.ApplicationTypes.Select(x => session.Get<ApplicationType>(x)).ToArray() : null;
            bool alreadyExists = false;
            var user  = session.Query<ApiMember>().Where(m => m.Email.ToLower() == cmd.Email.ToLower()).SingleOrDefault();
            if(user != null)
            {
                alreadyExists = true;
                user.Update(cmd.Email, cmd.FirstName, cmd.LastName, cmd.Gender, cmd.AgeRange, country, cmd.Zip, appTypes);
            }
            else
            {
                user = new ApiMember(cmd.Email, cmd.FirstName, cmd.LastName, cmd.Gender, cmd.AgeRange, country, cmd.Zip, appTypes);
            }
            session.Save(user);

            return new CreateThirdPartyAPIMemberCommand.Result { Id = user.Id, AlreadyExists = alreadyExists };
        }
    }
}
