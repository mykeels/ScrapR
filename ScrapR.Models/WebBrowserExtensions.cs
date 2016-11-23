using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using mshtml;

namespace ScrapR.Models
{

    public static class WebBrowserExtensions
    {
        const int POLL_DELAY = 500;

        // navigate and download 
        public static async Task<string> NavigateAsync(this WebBrowser webBrowser, string url, CancellationToken token)
        {
            // navigate and await DocumentCompleted
            var tcs = new TaskCompletionSource<bool>();
            WebBrowserDocumentCompletedEventHandler handler = (s, arg) =>
                tcs.TrySetResult(true);

            using (token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: true))
            {
                webBrowser.DocumentCompleted += handler;
                try
                {
                    webBrowser.Navigate(url);
                    await tcs.Task; // wait for DocumentCompleted
                }
                finally
                {
                    webBrowser.DocumentCompleted -= handler;
                }
            }

            // get the root element
            var documentElement = webBrowser.Document.GetElementsByTagName("html")[0];
            // poll the current HTML for changes asynchronosly
            var html = documentElement.OuterHtml;
            while (true)
            {
                // wait asynchronously, this will throw if cancellation requested
                await Task.Delay(POLL_DELAY, token);

                // continue polling if the WebBrowser is still busy
                if (webBrowser.IsBusy)
                    continue;

                var htmlNow = documentElement.OuterHtml;
                if (html == htmlNow)
                    break; // no changes detected, end the poll loop

                html = htmlNow;
            }

            // consider the page fully rendered 
            token.ThrowIfCancellationRequested();
            return html;
        }

        public static async Task<T> NavigateAsync<T>(this WebBrowser webBrowser, string url, CancellationToken token, Func<WebBrowser, T> func)
        {
            await NavigateAsync(webBrowser, url, token);

            return func(webBrowser);
            //return html;
        }

        public static async Task<string> GetLocationChangeAsync(this WebBrowser webBrowser, string url, CancellationToken token)
        {
            // navigate and await DocumentCompleted
            var tcs = new TaskCompletionSource<bool>();
            WebBrowserDocumentCompletedEventHandler handler = (s, arg) =>
                tcs.TrySetResult(true);

            using (token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: true))
            {
                webBrowser.DocumentCompleted += handler;
                try
                {
                    webBrowser.Navigate(url);
                    await tcs.Task; // wait for DocumentCompleted
                }
                finally
                {
                    webBrowser.DocumentCompleted -= handler;
                }
            }

            // get the root element
            var documentElement = webBrowser.Document.GetElementsByTagName("html")[0];
            string browserUrl = webBrowser.Url.ToString();
            if (browserUrl != url) return browserUrl; //location has already changed


            // poll the current location url for changes asynchronosly
            while (true)
            {
                // wait asynchronously, this will throw if cancellation requested
                await Task.Delay(POLL_DELAY, token);

                // continue polling if the WebBrowser is still busy
                if (webBrowser.IsBusy)
                    continue;

                string browserUrlNow = webBrowser.Url.ToString();
                if (browserUrl != browserUrlNow) //change detected
                {
                    browserUrl = browserUrlNow;
                    break;
                }
                
            }

            // consider the page fully rendered 
            token.ThrowIfCancellationRequested();
            return browserUrl;
        }

        public static async Task<string> GetLocationChangeAsync(this WebBrowser webBrowser, CancellationToken token, string currentUrl = null)
        {
            string browserUrl = webBrowser.Url.ToString();
            if (!String.IsNullOrEmpty(currentUrl) && currentUrl != browserUrl) return browserUrl; //url has already changed

            while (true)
            {
                await Task.Delay(POLL_DELAY, token);
                if (webBrowser.IsBusy)
                    continue;

                string browserUrlNow = webBrowser.Url.ToString();
                if (browserUrl != browserUrlNow) //change detected
                {
                    browserUrl = browserUrlNow;
                    break;
                }
            }
            token.ThrowIfCancellationRequested();
            return browserUrl;
        }

        public static T ExecuteScript<T>(this WebBrowser webBrowser, string scriptData, string funcName, object[] args = null)
        {
            if (!String.IsNullOrEmpty(scriptData)) webBrowser.InjectScript(scriptData);
            T ret = (T)webBrowser.Document.InvokeScript(funcName, args);
            return ret;
        }

        public static void InjectScript(this WebBrowser webBrowser, string scriptData)
        {
            HtmlElement body = webBrowser.Document.GetElementsByTagName("body")[0];
            HtmlElement scriptElem = webBrowser.Document.CreateElement("script");
            IHTMLScriptElement scriptDom = (IHTMLScriptElement)scriptElem.DomElement;
            scriptDom.text = scriptData;
            body.AppendChild(scriptElem);
        }

        // enable HTML5 (assuming we're running IE10+)
        // more info: http://stackoverflow.com/a/18333982/1768303
        public static void SetFeatureBrowserEmulation()
        {
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Runtime)
                return;
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                appName, 10000, RegistryValueKind.DWord);
        }
    }
}
