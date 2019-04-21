using Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Utils
{
    public class RegistrationConfirmation
    {
        private Dictionary<string, Dictionary<string,string>> _ConfirmationKeys = new Dictionary<string, Dictionary<string, string>>();

        public string CreateConfirmationUrl(Dictionary<string, string> owner)
        {
            Random random = new Random();
            string key = (char)('a' + random.Next(1, 26))
                    + (char)('a' + random.Next(1, 26))
                    + random.Next(101, 999).ToString()
                    + (char)('a' + random.Next(1, 26))
                    + (char)('a' + random.Next(1, 26))
                    + random.Next(10, 99).ToString();
            string url = @"http://localhost:56972/api/registration/" + key;
            _ConfirmationKeys.Add(key, owner);
            return url;
        }

        public Dictionary<string, string> CheckKey(string confirmationKey)
        {
            try
            {
                var owner = _ConfirmationKeys[confirmationKey];
                return owner;
            }
            catch
            {
                return null;
            }
        }
    }
}
