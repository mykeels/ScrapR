using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using ScrapR.Models.TrvBeta;
using ScrapR.Web.Api.Models;

namespace ScrapR.Web.Api.Controllers
{
    public class TrvBetaController : ApiController
    {
        // GET api/values
        public async Task<Dictionary<string, string>> Get()
        {
            var cts = new CancellationTokenSource((int)TimeSpan.FromMinutes(3).TotalMilliseconds);
            return await Scrapper.Create().GetFlightDataAsync(Query.GetSample(), cts.Token);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public Response<Routes> Post([FromBody]Query query)
        {
            if (query == null) return Response<Routes>.Create("Invalid Request", null, false);
            return Response<Routes>.Create(Scrapper.Create().GetFlightData(query), true);
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