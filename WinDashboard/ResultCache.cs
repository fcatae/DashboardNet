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
        List<SiteUrl> m_websites = new List<SiteUrl>();
        Dictionary<string, int> m_idxWebsiteName = new Dictionary<string, int>();

        public IList<SiteUrl> Websites
        {
            get { return m_websites; }
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

        void AddWebsite(string site)
        {
            int index = m_websites.Count;

            m_websites.Add(new SiteUrl(site));

            m_idxWebsiteName.Add(site, index);
        }

        public void Update(SiteResult result)
        {
            int index = -1;

            index = m_idxWebsiteName[result.url];

            m_websites[index] = result;
        }
    }
}
