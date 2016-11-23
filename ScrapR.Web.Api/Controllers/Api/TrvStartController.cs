using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models.TrvStart;
using ScrapR.Web.Api.Models;

namespace ScrapR.Web.Api.Controllers
{
    public class TrvStartController : ApiController
    {
        // GET api/values
        public async Task<Response<List<Itinerary>>> Get()
        {
            ScrapR.Models.WebBrowserExtensions.SetFeatureBrowserEmulation();
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);
            //return Scrapper.Create().RunTask<List<Itinerary>>(Scrapper.Create().GetItinerariesAsync(Query.GetSampleQuery(), cts.Token));
            return Response<List<Itinerary>>.Create(await Scrapper.Create().GetItinerariesAsync(Query.GetSampleQuery(), cts.Token), true);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public async Task<Response<List<Itinerary>>> Post([FromBody]Query query)
        {
            if (query == null) return Response<List<Itinerary>>.Create("Invalid Request");

            ScrapR.Models.WebBrowserExtensions.SetFeatureBrowserEmulation();
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);
            return Response<List<Itinerary>>.Create(await Scrapper.Create().GetItinerariesAsync(query, cts.Token), true);
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