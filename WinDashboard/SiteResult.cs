using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDashboard
{
    class SiteResult
    {
        public string       url;
        public DateTime     scanTime;
        public string       raw_content;
        public int checkTotal;
        public int checkSuccess;
        Dictionary<string, SiteResultCheck>  checklist = new Dictionary<string, SiteResultCheck>();

        public SiteResultCheck checkBrowserDetection;
        public SiteResultCheck checkCSSPrefixes;
        public SiteResultCheck checkEdge;
        public SiteResultCheck checkJsLibs;
        public SiteResultCheck checkPluginFree;
        public SiteResultCheck checkMarkup;

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

            checkBrowserDetection = checklist["browserDetection"];
            checkCSSPrefixes = checklist["cssprefixes"];
            checkEdge = checklist["edge"];
            checkJsLibs = checklist["jslibs"];
            checkPluginFree = checklist["pluginfree"];
            checkMarkup = checklist["markup"];
        }
    }
}
