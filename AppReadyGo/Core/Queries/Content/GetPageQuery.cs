using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetPageQuery : IQuery<PageResult>
    {
        public string Url { get; private set; }

        public GetPageQuery(string url)
        {
            this.Url = url;
        }
    }
}
