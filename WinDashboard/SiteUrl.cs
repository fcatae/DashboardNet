using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDashboard
{
    interface ISiteEntry
    {
        string url { get; }
    }

    class SiteUrl : ISiteEntry
    {
        public string url { get; }

        public SiteUrl(string url)
        {
            this.url = url;
        }
    }
}
