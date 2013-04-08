using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.QueryResults
{
    public class ApplicationDetailsResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ApplicationType Type { get; set; }
    }
}
