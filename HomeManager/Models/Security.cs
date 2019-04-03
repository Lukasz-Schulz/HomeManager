using Home;
using HomeManagerSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class Security
    {
        List<Session> _Sessions;

        public Security()
        {
            _Sessions = new List<Session>();
        }

        public string Encrypt(string s)
        {
            var encrypter = new Encrypter();
            return encrypter.Encrypt(s);
        }

        private bool CheckCredentials(string login, string password)
        {
            DatabaseConnection database = new DatabaseConnection();
            var user = database.GetUserData(login);
            try
            {
                if (user["Password"].Equals(Encrypt( password)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public string Login(string login, string password)
        {
            if (!(_Sessions.FindIndex(s => s.User == login) >= 0))
            {
                bool isLogged = CheckCredentials(login, password);
                if (isLogged)
                {

                    return "success";
                }
                else return "failure";
            }
            else return "already logged in";
        }

        private void CreateSession(string user)
        {
            if (!(_Sessions.FindIndex(s => s.User == user) >= 0))
            {
                _Sessions.Add(new Session(user));
            }
            else
            {
                int index = _Sessions.FindIndex(s => s.User == user);
                _Sessions[index].CheckSession();
            }
        }

        public bool Logout(string user)
        {
            try
            {
                int index = _Sessions.FindIndex(s => s.User == user);
                _Sessions[index].DestroySession();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
