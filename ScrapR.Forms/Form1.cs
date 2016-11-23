using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using ScrapR.Models;

namespace ScrapR.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //webBrowser1.ScriptErrorsSuppressed = true;
            this.Width = 1366;
            //System.Diagnostics.Debug.WriteLine(ScrapR.Models.TrvStart.Query.GetSampleQuery().ToJson());
            WebBrowserExtensions.SetFeatureBrowserEmulation();
            webBrowser1.Navigate(Models.Wkn.Query.GetSampleQuery().ToString());
            webBrowser1.DocumentCompleted += (object s, WebBrowserDocumentCompletedEventArgs ev) =>
            {
                webBrowser1.InjectScript(Resources.JSON);
            };
            webBrowser1.Navigated += (object s, WebBrowserNavigatedEventArgs ev) =>
            {
                this.Text = ev.Url.ToString();
            };
        }
    }
}
