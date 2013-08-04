using AppReadyGo.Core.QueryResults.Admin;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetItemQuery : IQuery<ParentItemResult>
    {
        public int Id { get; private set; }

        public GetItemQuery(int id)
        {
            this.Id = id;
        }
    }
}
