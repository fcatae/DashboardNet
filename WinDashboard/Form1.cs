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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] items = { "1" , "2"};

            var item = new ListViewItem("1");
            item.SubItems.Add("2");
            item.SubItems.Add("3");
            item.SubItems.Add("4");

            listView1.Items.Add(item);

            //Task analysis = AnalyzeUrl("http://localhost:1337");
        }

        async Task<string> AnalyzeUrl(string url)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync("/api/v2/scan?url=http://www.lab27.com.br");

            var content = await response.Content.ReadAsStringAsync();

            dynamic obj = JsonConvert.DeserializeObject(content);

            return content;
        }
    }
}
