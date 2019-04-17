using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Home;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HomeManager.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace HomeManager.Controllers
{

    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        Security _security = new Security();
        public LoginController()
        {
        }

        //TEST COLLECTIONS

        static BasicHomeFactory basicHomeFactory = new BasicHomeFactory();
        static List<Home.Home> TestList = new List<Home.Home>() { basicHomeFactory.TestHome(), basicHomeFactory.TestHome() };
        JArray TestArray = JArray.Parse(JsonConvert.SerializeObject(TestList));

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return TestArray.ToString();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            try
            {
                var response = TestArray[int.Parse(id)];
                return response.ToString();
            }
            catch
            {
                return "No content";
            }
        }

        // POST api/values
        [HttpPost("{id}")]
        public string Post([FromForm]Dictionary<string, string> value, string id)
        {
            string response;
            if (id.Equals("login"))
            {
                response = _security.Login(value["Email"], value["password"]);
                return response;
            }
            else if (id.Equals("logout"))
            {
                bool success = _security.Logout(value["sessionKey"]);
                return success.ToString();
            }
            else
            {
                return Program.TheSessionHolder.ProlongSession(value["sessionKey"]).ToString();
            }
        }
    }
}
