﻿using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnalyzeUrl("http://www.microsoft.com");
        }

        async Task<string> AnalyzeUrl(string url)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync("/");

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
