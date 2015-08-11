using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDashboard
{
    class SiteResult : ISiteEntry
    {
        public string       url { get; }
        public DateTime     scanTime;
        public string       raw_content;
        public int checkTotal;
        public int checkSuccess;
        Dictionary<string, SiteResultCheck>  checklist = new Dictionary<string, SiteResultCheck>();

        public SiteResultCheck checkBrowserDetectionData;
        public SiteResultCheck checkCSSPrefixesData;
        public SiteResultCheck checkEdgeData;
        public SiteResultCheck checkJsLibsData;
        public SiteResultCheck checkPluginFreeData;
        public SiteResultCheck checkMarkupData;
        
        public SiteResult(string content)
        {
            raw_content = content;

            scanTime = DateTime.Now;

            dynamic contentTree = JsonConvert.DeserializeObject(content);

            url = contentTree.url.uri;            

            foreach (var result in contentTree.results)
            {
                string checkName = result.Name;

                var checkResult = new SiteResultCheck()
                {
                    name = result.Name,
                    passed = result.Value.passed,
                    data = result.Value.data.ToString()
                };
                
                checklist[checkName] = checkResult;

                if(checkResult.passed)
                {
                    checkSuccess++;
                }

                checkTotal++;
            }

            checkBrowserDetectionData = checklist["browserDetection"];
            checkCSSPrefixesData = checklist["cssprefixes"];
            checkEdgeData = checklist["edge"];
            checkJsLibsData = checklist["jslibs"];
            checkPluginFreeData = checklist["pluginfree"];
            checkMarkupData = checklist["markup"];
        }
    }
}
