using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDashboard
{
    class SiteQuickResult : ISiteEntry
    {
        public string url { get; }

        public bool checkBrowserDetection;
        public bool checkCSSPrefixes;
        public bool checkEdge;
        public bool checkJsLibs;
        public bool checkPluginFree;
        public bool checkMarkup;
    }
}
