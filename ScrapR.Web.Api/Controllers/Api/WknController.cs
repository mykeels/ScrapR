using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Web.Api.Models;
using ScrapR.Models.Wkn;

namespace ScrapR.Web.Api.Controllers
{
    public class WknController : ApiController
    {
        // GET api/values
        public async Task<Response<Roots>> Get()
        {
            ScrapR.Models.WebBrowserExtensions.SetFeatureBrowserEmulation();
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);
            return Response<Roots>.Create("success", await Scrapper.Create().GetFlightsDataAsync(Query.GetSampleQuery().ToString(), cts.Token), false);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public async Task<Response<Roots>> Post([FromBody]Query query)
        {
            if (query == null) return Response<Roots>.Create("Invalid Request", null, false);
            ScrapR.Models.WebBrowserExtensions.SetFeatureBrowserEmulation();
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);
            return Response<Roots>.Create("success", await Scrapper.Create().GetFlightsDataAsync(query.ToString(), cts.Token), true);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}