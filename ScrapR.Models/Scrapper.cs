using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace ScrapR.Models
{
    public class Scrapper
    {
        public async Task<T> ExecutePageAsync<T>(string url, CancellationToken token, Func<WebBrowser, T> func)
        {
            using (var apartment = new MessageLoopApartment())
            {
                // create WebBrowser inside MessageLoopApartment
                var webBrowser1 = apartment.Invoke(() => new WebBrowser());

                try
                {
                    //Console.WriteLine("URL:\n" + url);
                    var navigationCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                    navigationCts.CancelAfter((int)TimeSpan.FromSeconds(30).TotalMilliseconds);
                    var navigationToken = navigationCts.Token;

                    // run the navigation task inside MessageLoopApartment
                    T ret = await apartment.Run(() =>
                    {
                        webBrowser1.Width = 1366;
                        webBrowser1.Height = 768;
                        webBrowser1.ScriptErrorsSuppressed = true;
                        return webBrowser1.NavigateAsync<T>(url, navigationToken, func);
                    }, navigationToken);

                    return ret;
                }
                finally
                {
                    // dispose of WebBrowser inside MessageLoopApartment
                    apartment.Invoke(() => webBrowser1.Dispose());
                }
            }
        }

        protected async Task<string> GetScriptDataAsync(string url, CancellationToken token, string scriptFn, string funcName)
        {
            return await this.ExecutePageAsync<string>(url, token, (browser) =>
            {
                return browser.ExecuteScript<string>(scriptFn, funcName);
            });
        }

        public async Task<string> GetLocationChangeAsync(string url, CancellationToken token)
        {
            using (var apartment = new MessageLoopApartment())
            {
                // create WebBrowser inside MessageLoopApartment
                var webBrowser1 = apartment.Invoke(() => new WebBrowser());

                try
                {
                    //Console.WriteLine("URL:\n" + url);
                    var navigationCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                    navigationCts.CancelAfter((int)TimeSpan.FromSeconds(30).TotalMilliseconds);
                    var navigationToken = navigationCts.Token;

                    // run the navigation task inside MessageLoopApartment
                    string newLocation = await apartment.Run(() =>
                    {
                        webBrowser1.ScriptErrorsSuppressed = true;
                        return webBrowser1.GetLocationChangeAsync(url, navigationToken);
                    }, navigationToken);

                    return newLocation;
                }
                finally
                {
                    // dispose of WebBrowser inside MessageLoopApartment
                    apartment.Invoke(() => webBrowser1.Dispose());
                }
            }
        }

        public T RunTask<T>(Task<T> execTask)
        {
            try
            {
                WebBrowserExtensions.SetFeatureBrowserEmulation(); // enable HTML5
                var task = Task.Run(async () => await execTask);
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                while (ex is AggregateException && ex.InnerException != null)
                    ex = ex.InnerException;
                Console.WriteLine(ex.Message);
                //Environment.Exit(-1);
                return default(T);
            }
        }

        public void RunTask(Task execTask)
        {
            WebBrowserExtensions.SetFeatureBrowserEmulation(); // enable HTML5
            var task = Task.Run(async () => await execTask);
            task.Wait();
        }
    }
}
