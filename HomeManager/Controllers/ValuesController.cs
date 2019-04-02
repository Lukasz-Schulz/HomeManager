using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Home;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HomeManager.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {


            JObject o1 = JObject.Parse(System.IO.File.ReadAllText(@"Models/exampleInput.json"));
            //usunac
            BasicHomeFactory homeFactory = new BasicHomeFactory();
            //Home.Home home = homeFactory.CreateHomeFromDataBase("8902");

            //DatabaseConnection database = new DatabaseConnection();

            //database.AddHomeToDatabase(home);

            //foreach (var c in home._Counters)
            //{
            //    database.AddCounterToDatabase(c);
            //    database.AddProviderToDatabase(c._Provider);
            //}

            //foreach (var c in home._Contracts)
            //{
            //    database.AddContractToDatabase(c);
            //}

            //database.AddOwnerToDatabase(home._Owner);

            //var resp = database.GetCountersData("112233");
            //var json = JsonConvert.SerializeObject(resp);
            //database.CreateProvidersTable();
            //database.AddOwnerToDatabase(new BasicOwner() { ID = 345, _FirstName = "E", _SecondName = "S", _Email = "ES@es.es" });
            return new string[] { "value1", o1.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            BasicHomeFactory basicHomeFactory = new BasicHomeFactory();
            //var testHome = basicHomeFactory.CreateHomeFromDataBase(id);
            //DatabaseConnection databaseConnection = new DatabaseConnection();
            //string response = databaseConnection.TESTselect(id);

            //JObject o1 = JObject.Parse(System.IO.File.ReadAllText(@"Models/exampleInput.json"));
            //string jsonResponse = JsonConvert.SerializeObject(testHome);
            var resp = basicHomeFactory.TestHome();
            var json = JsonConvert.SerializeObject(resp);
            return JObject.Parse(json).ToString();
        }
        
        // POST api/values
        [HttpPost]
        public void Post([FromForm]Dictionary<string,string> value, [FromHeader]string header)
        {
            var test = value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
