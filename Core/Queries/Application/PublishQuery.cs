using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.Queries.Analytics
{
    public class TaskQuery : IQuery<PublishResult>
    {
        public int ApplicationId { get; set; }

        public TaskQuery(int applicationId)
        {
            this.ApplicationId = applicationId;
        }
    }
}
