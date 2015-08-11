﻿using Newtonsoft.Json;
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

        public Form1()
        {
            m_scanner = new Scanner("http://localhost:1337");

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var item = new ListViewItem("www.microsoft.com");
            listView1.Items.Add(item);

            ProcessItemAsync(item);
        }
        
        async Task ProcessItemAsync(ListViewItem item)
        {
            var shortUrl = item.Text;
            var url = "http://" + item.Text;
            
            var result = await AnalyzeUrl(url);
            
            item.SubItems.Clear();
            item.SubItems.Add(result.checkBrowserDetection.passed.ToString());
            item.SubItems.Add(result.checkCSSPrefixes.passed.ToString());
            item.SubItems.Add(result.checkEdge.passed.ToString());
            item.SubItems.Add(result.checkJsLibs.passed.ToString());
            item.SubItems.Add(result.checkPluginFree.passed.ToString());
            item.SubItems.Add(result.checkMarkup.passed.ToString());

            item.Text = shortUrl;
        }

        async Task<SiteResult> AnalyzeUrl(string url)
        {
            return await m_scanner.AnalyzeUrl(url);            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            foreach (var item in listView1.SelectedItems)
            {
                ProcessItemAsync((ListViewItem)item);
                //MessageBox.Show("selected " + ((ListViewItem)item).Text);
            }
        }
    }
}
