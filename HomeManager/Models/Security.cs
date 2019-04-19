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
        public string Encrypt(string s)
        {
            var encrypter = new Encrypter();
            return encrypter.Encrypt(s);
        }

        private bool CheckCredentials(string email, string password)
        {
            DatabaseConnection database = new DatabaseConnection();
            var user = database.GetEncryptedPasswordAndEmail(email);
            try
            {
                var encrypted = Encrypt(password).Trim();
                if (user["Password"].Trim().Equals(encrypted))
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

        public string Login(string email, string password)
        {
            if (!Program.TheSessionHolder.CheckIfSessionForUserExists(email))
            {
                bool isCorrect = CheckCredentials(email, password);
                if (isCorrect)
                {
                    string newSessionKey = Program.TheSessionHolder.CreateSession(email);
                    return newSessionKey;
                }
                else return "failure";
            }
            else return "already logged in";
        }

        private void CreateSession(string user)
        {
            if (!Program.TheSessionHolder.CheckIfSessionForUserExists(user))
            {
                Program.TheSessionHolder.CreateSession(user);
            }
        }

        public bool Logout(string sessionKey)
        {
            try
            {
                Program.TheSessionHolder.DropSession(sessionKey);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
