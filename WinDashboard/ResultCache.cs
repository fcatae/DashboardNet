﻿using System;
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
                        m_websites.Add(new SiteUrl(site));
                    }
                }
            }
        }
    }
}
