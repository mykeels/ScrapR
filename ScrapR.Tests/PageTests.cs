using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScrapR.Tests
{
    [TestClass]
    public class PageTests
    {
        public void wknLoadsSuccessfully()
        {
            testLoadPage("http://www.wakanow.com/en-ng/");
        }

        public void trvStartLoadsSuccessfully()
        {
            testLoadPage("http://www.travelstart.com");
        }

        public void trvBetaLoadsSuccessfully()
        {
            testLoadPage("http://www.travelbeta.com");
        }

        public void trvFixLoadsSuccessfully()
        {
            testLoadPage("http://www.travelfix.com");
        }

        private void testLoadPage(string url)
        {
            Api.Execute(url).Success((browser) =>
            {
                Assert.AreNotEqual<string>("", browser.DocumentTitle);
                Assert.AreNotEqual<string>("Navigation Canceled", browser.DocumentTitle);
                Assert.IsNotNull(browser.DocumentText);
                Assert.IsNotNull(browser.DocumentTitle);
                Debug.WriteLine($"Document Title: {browser.DocumentTitle}");
                string line = Enumerable.Repeat("=", 30).Aggregate((a, b) => a + b);
                Debug.WriteLine($"{line} Document Body {line}");
                Debug.WriteLine(browser.DocumentText);
            }).Wait();
        }
    }
}
