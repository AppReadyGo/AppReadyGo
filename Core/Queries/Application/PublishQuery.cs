using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.Queries.Analytics
{
    public class PublishQuery : IQuery<PublishResult>
    {
        public int ApplicationId { get; set; }

        public PublishQuery(int applicationId)
        {
            this.ApplicationId = applicationId;
        }
    }
}
