using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class SessionHolder
    {
        protected class Session
        {
            public Session(string login)
            {
                    Start = DateTime.Now;
                    ProlongueSession();
                    User = login;
                    SessionKey = login +"+"+ Start.ToShortDateString() +"+"+ Start.ToShortTimeString();
            }

            public DateTime Start { get; private set; }
            public DateTime Expiry { get; private set; }
            public string User { get; private set; }
            public string SessionKey { get; private set; }

            public void ProlongueSession()
            {
                Expiry = DateTime.Now.AddMinutes(5);
            }
        }
    
        private Dictionary<string, Session> _Sessions = new Dictionary<string, Session>();

        public string CreateSession(string user)
        {
            if (user.Length > 0)
            {
                Session session = new Session(user);
                _Sessions.Add(session.SessionKey, session);
                return session.SessionKey;
            }
            else
            {
                return null;
            }
        }

        public bool ProlongSession(string sessionKey)
        {
            if (sessionKey.Length > 0)
            {
                try
                {
                    var expiry = _Sessions[sessionKey].Expiry;
                    if (expiry > DateTime.Now)
                    {
                        _Sessions[sessionKey].ProlongueSession();
                        return true;
                    }
                    else
                    {
                        _Sessions.Remove(sessionKey);
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool CheckIfSessionForUserExists(string user)
        {
            if( _Sessions.FirstOrDefault(session => session.Value.User == user).Key != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DropSession(string sessionkey)
        {
            _Sessions.Remove(sessionkey);
        }
    }
}
