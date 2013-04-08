using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetSystemMailQuery : IQuery<MailResult>
    {
        public string Url { get; private set; }

        public GetSystemMailQuery(string url)
        {
            this.Url = url;
        }
    }
}
