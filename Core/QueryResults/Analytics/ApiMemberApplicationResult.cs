using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.QueryResults.Analytics
{
    public class ApiMemberApplicationResult : UserDetailsResult
    {
        public int UserId { get; set; }

        public string Review { get; set; }
    }
}
