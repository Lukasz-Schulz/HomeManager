using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            string url = Program.TheRegistrationConfirmation.CreateConfirmationUrl(profile);
            new Utils.EmailSender().SendEmailAsync(profile["email"], url);
            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }

        // GET api/values/5
        [HttpGet("{confirmationKey}")]
        public IActionResult Get(string confirmationKey)
        {
            var profile = Program.TheRegistrationConfirmation.CheckKey(confirmationKey);
            if (profile != null)
            {
                return _registrator.Register(profile);
            }
            else return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
    }
}
