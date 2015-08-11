using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDashboard
{
    class ResultCache
    {
        List<ISiteEntry> m_websites;
        Dictionary<string, int> m_idxWebsiteName;

        public ResultCache()
        {
            Clear();
        }

        public IList<ISiteEntry> Websites
        {
            get { return m_websites; }
        }

        public void Clear()
        {
            m_websites = new List<ISiteEntry>();
            m_idxWebsiteName = new Dictionary<string, int>();
        }

        public void LoadWebsites(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string site;
                
                while ((site=reader.ReadLine()) != null)
                {
                    // make sure it starts with HTTP/HTTPS
                    if (!site.ToLower().Contains("://"))
                    {
                        site = "http://" + site;
                    }

                    site = site.Trim();

                    if (site != "")
                    {
                        AddWebsite(site);
                    }
                }
            }
        }

        public void LoadResultsCsv(string filename)
        {

        }
        public void ExportResultsCsv(string filename)
        {
            string HEADER = "url,browserDetection,cssprefixes,edge,jslibs,pluginfree,markup";
            string BODY_FORMAT = "{0},{1},{2},{3},{4},{5},{6}";

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(HEADER);

                foreach (var entry in m_websites)
                {
                    var result = entry as SiteResult;

                    if(result != null)
                    {
                        writer.WriteLine(String.Format(BODY_FORMAT, result.url,
                            result.checkBrowserDetectionData.passed ? 1 : 0,
                            result.checkCSSPrefixesData.passed ? 1 : 0,
                            result.checkEdgeData.passed ? 1 : 0,
                            result.checkJsLibsData.passed ? 1 : 0,
                            result.checkPluginFreeData.passed ? 1 : 0,
                            result.checkMarkupData.passed ? 1 : 0
                            ));
                    }
                }
            }
        }

        void AddWebsite(string site)
        {
            int index = m_websites.Count;

            if(!m_idxWebsiteName.ContainsKey(site))
            {
                m_websites.Add(new SiteUrl(site));

                m_idxWebsiteName.Add(site, index);
            }
        }

        public void Update(SiteResult result)
        {
            int index = -1;
            string url = result.url;

            if(!m_idxWebsiteName.ContainsKey(url))
            {
                AddWebsite(url);
            }

            index = m_idxWebsiteName[url];

            m_websites[index] = result;
        }

        public SiteResult TryGetResult(string url)
        {
            int index;
            SiteResult result = null;

            if (m_idxWebsiteName.TryGetValue(url, out index))
            {
                result = m_websites[index] as SiteResult;
            }

            return result;
        }
    }
}
