using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WinDashboard
{
    class Scanner
    {
        HttpClient m_client;
        string m_scanUrl;

        public Scanner(string scannerlUrl)
        {
            m_scanUrl = scannerlUrl;

            m_client = new HttpClient() {
                BaseAddress = new Uri(scannerlUrl)
            };
        }

        public async Task<SiteResult> AnalyzeUrl(string url)
        {
            var response = await m_client.GetAsync("/api/v2/scan?url=" + url);

            var content = await response.Content.ReadAsStringAsync();

            var result = new SiteResult(content);

            return result;
        }
    }
}
