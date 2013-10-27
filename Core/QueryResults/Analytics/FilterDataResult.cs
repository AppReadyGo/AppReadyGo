using System.Collections.Generic;
using System.Drawing;
using AppReadyGo.Core.QueryResults.Applications;

namespace AppReadyGo.Core.QueryResults.Analytics.QueryResults
{
    public class FilterDataResult
    {
        public IEnumerable<ExtendedApplicationResult> Applications { get; set; }
        public List<Size> ScreenSizes { get; set; }
        public List<string> Pathes { get; private set; }
        public List<string> Languages { get; private set; }
        public List<string> OperatingSystems { get; private set; }
        public List<string> Countries { get; private set; }
        public List<string> Cities { get; private set; }
        public Screen ScreenData { get; set; }

        public int SelectedApplicationId { get; set; }
        public string SelectedPath { get; set; }
        public Size? SelectedScreenSize { get; set; }

        public IEnumerable<ApiMemberApplicationResult> ApiMemberApplications { get; set; }

        public class Screen
        {
            public int? Id { get; set; }

            public string FileExtention { get; set; }


            /// <summary>
            /// Click on this screen in this time span
            /// </summary>
            public int ClicksAmount { get; set; }

            /// <summary>
            /// Clicks on this screen in this time span
            /// </summary>
            public int ScrollsAmount { get; set; }

            /// <summary>
            /// Any scrolls on this screen ever
            /// </summary>
            public bool HasScrolls { get; set; }

            /// <summary>
            /// Any scrools on this sceen ever
            /// </summary>
            public bool HasClicks { get; set; }

            public int VisitsAmount { get; set; }

            /// <summary>
            /// Control Clicks on this screen in this time span
            /// </summary>
            public int ControlClicksAmount { get; set; }


            /// <summary>
            /// Any Control Clicks on this screen in this time span
            /// </summary>
            public bool HasControlClicks { get; set; }
        }
    }
}
