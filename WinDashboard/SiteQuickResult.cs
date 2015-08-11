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

        public SiteQuickResult(string url, bool checkBrowserDetection, bool checkCSSPrefixes, bool checkEdge, bool checkJsLibs, bool checkPluginFree, bool checkMarkup)
        {
            this.url = url;
            this.checkBrowserDetection = checkBrowserDetection;
            this.checkCSSPrefixes = checkCSSPrefixes;
            this.checkEdge = checkEdge;
            this.checkJsLibs = checkJsLibs;
            this.checkPluginFree = checkPluginFree;
            this.checkMarkup = checkMarkup;
        }
    }
}
