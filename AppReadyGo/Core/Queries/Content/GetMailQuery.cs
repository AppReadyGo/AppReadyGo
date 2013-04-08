using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetMailQuery : IQuery<MailResult>
    {
        public string Url { get; private set; }

        public GetMailQuery(string url)
        {
            this.Url = url;
        }
    }
}
