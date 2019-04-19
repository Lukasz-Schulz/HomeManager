using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Registration")]
    public class RegistrationController : Controller
    {
        private readonly Registrator _registrator = new Registrator();
        // POST: api/Registration
        [HttpPost]
        public IActionResult Post([FromForm]Dictionary<string,string> profile)
        {
            return _registrator.Register(profile);
        }
    }
}
