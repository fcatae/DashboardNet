using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinDashboard
{
    public partial class Form1 : Form
    {
        Scanner m_scanner;
        ResultCache m_cache;

        public Form1()
        {
            m_scanner = new Scanner("http://localhost:1337");
            m_cache = new ResultCache();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_cache.Clear();
            m_cache.LoadWebsites("websites.csv");

            listView1.Items.Clear();

            foreach (var site in m_cache.Websites)
            {
                listView1.Items.Add(site.url);
            }

        }
        
        async Task ScanWebsitesAsync()
        {
            foreach (var item in listView1.Items)
            {
                await ProcessItemAsync((ListViewItem)item);
            }
        }

        async Task ProcessItemAsync(ListViewItem item)
        {
            var shortUrl = item.Text;
            var url = item.Text;

            string PROCESSING = "Processing ";
            if ( !item.Text.StartsWith(PROCESSING))
            {
                item.Text = PROCESSING + shortUrl;
                item.ForeColor = Color.Red;
                item.Selected = false;

                SiteResult result;

                try
                {
                    result = await AnalyzeUrl(url);
                }
                catch (Exception exception)
                {
                    item.SubItems.Clear();
                    item.Text = shortUrl;
                    item.SubItems.Add(exception.Message);

                    return;
                }
                
                item.ForeColor = Color.Black;

                item.SubItems.Clear();
                item.SubItems.Add("");
                item.SubItems.Add(result.checkBrowserDetectionData.passed.ToString());
                item.SubItems.Add(result.checkCSSPrefixesData.passed.ToString());
                item.SubItems.Add(result.checkEdgeData.passed.ToString());
                item.SubItems.Add(result.checkJsLibsData.passed.ToString());
                item.SubItems.Add(result.checkPluginFreeData.passed.ToString());
                item.SubItems.Add(result.checkMarkupData.passed.ToString());

                item.Text = shortUrl;
            }

        }

        async Task<SiteResult> AnalyzeUrl(string url)
        {
            SiteResult result;

            result = m_cache.TryGetResult(url);

            if (result == null)
            {
                result = await m_scanner.AnalyzeUrl(url);

                m_cache.Update(result);
            }

            return result;       
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            foreach (var item in listView1.SelectedItems)
            {
                Task task = ProcessItemAsync((ListViewItem)item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task task = ScanWebsitesAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_cache.ExportResultsCsv("results.csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_cache.LoadResultsCsv("results.csv");
        }
    }
}
