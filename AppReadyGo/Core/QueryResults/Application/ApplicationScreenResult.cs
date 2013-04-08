using AppReadyGo.Core.Entities;
using System.Drawing;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ApplicationScreenResult
    {
        public int Id { get; set; }

        public Size ScreenSize { get; set; }

        public string Path { get; set; }

        public int ApplicationId { get; set; }
    }
}
