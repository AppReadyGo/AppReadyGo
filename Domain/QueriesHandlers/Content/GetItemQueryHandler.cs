using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Admin;
using AppReadyGo.Core.QueryResults.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetItemQueryHandler : IQueryHandler<GetItemQuery, ParentItemResult>
    {
        public ParentItemResult Run(ISession session, GetItemQuery query)
        {
            var item = session.Query<Item>()
                            .Where(k => k.Id == query.Id)
                            .Select(k => new
                            {
                                Id = k.Id,
                                IsHTML = k.IsHTML,
                                SubKey = k.SubKey,
                                Value = k.Value,
                                Key = k.Key,
                                Page = k.Page,
                                Mail = k.Mail
                            })
                            .SingleOrDefault();

            if (item != null)
            {
                var res = new ParentItemResult
                {
                    Id = item.Id,
                    IsHTML = item.IsHTML,
                    SubKey = item.SubKey,
                    Value = item.Value
                };

                if(item.Key != null)
                {
                    res.ParentId = item.Key.Id;
                    res.ParentUrl = item.Key.Url;
                }
                else if(item.Page != null)
                {
                    res.ParentId = item.Page.Id;
                    res.ParentUrl = item.Page.Url;
                }
                else if(item.Mail != null)
                {
                    res.ParentId = item.Mail.Id;
                    res.ParentUrl = item.Mail.Url;
                }

                return res;
            }

            return null;
        }
    }
}
