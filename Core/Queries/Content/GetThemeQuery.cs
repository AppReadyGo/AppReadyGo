using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetThemeQuery : IQuery<ThemeResult>
    {
        public string Url { get; private set; }

        public GetThemeQuery(string url)
        {
            this.Url = url;
        }
    }
}
