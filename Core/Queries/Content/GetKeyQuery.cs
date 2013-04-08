using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetKeyQuery : IQuery<KeyResult>
    {
        public string Url { get; private set; }

        public GetKeyQuery(string url)
        {
            this.Url = url;
        }
    }
}
