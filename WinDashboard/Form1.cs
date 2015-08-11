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

        public Form1()
        {
            m_scanner = new Scanner("http://localhost:1337");

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

            Task analysis = AnalyzeUrl("http://localhost:1337", "http://www.microsoft.com");
        }
                
        async Task<string> AnalyzeUrl(string scanUrl, string url)
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
                MessageBox.Show("selected " + ((ListViewItem)item).Text);
            }
        }
    }
}
