using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeManager.Controllers
{
    [Produces("application/json")]
    [Route("api/GetHomes")]
    public class GetHomesController : Controller
    {        
        // POST: api/GetHomes
        [HttpPost]
        public JArray Post([FromForm]Dictionary<string, string> value)
        {
            try
            {
                var response = Program.TheSessionHolder.GetHome(value["sessionKey"]);
                JArray jsonArray = JArray.Parse(JsonConvert.SerializeObject(response));
                return jsonArray;

            }
            catch(Exception ex) {
                return JArray.Parse(JsonConvert.SerializeObject(new List<string>{ ex.Message }));
            }
        }
    }
}
