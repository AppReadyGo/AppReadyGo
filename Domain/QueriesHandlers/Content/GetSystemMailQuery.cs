using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetSystemMailQueryHandler : IQueryHandler<GetSystemMailQuery, MailResult>
    {
        public MailResult Run(NHibernate.ISession session, GetSystemMailQuery query)
        {
            var mail = session.Query<SystemMail>()
                            .Where(m => m.Url.ToLower() == query.Url.ToLower())
                            .Select(m => m)
                            .Single();
            return new MailResult
            {
                Id = mail.Id,
                Url = mail.Url,
                Body = mail.Body.Value,
                Subject = mail.Subject.Value,
                ThemeUrl = mail.Theme.Url
            };
        }
    }
}
