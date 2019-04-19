using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public partial class SessionHolder
    {
    
        private Dictionary<string, Session> _Sessions = new Dictionary<string, Session>();

        public string CreateSession(string user)
        {
            if (user.Length > 0)
            {
                Session session = new Session(user);
                if(_Sessions.TryAdd(session.SessionKey, session)) return session.SessionKey;
                else return null;
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

        public bool CheckIfSessionForUserExists(string email)
        {
            if( _Sessions.FirstOrDefault(session => session.Value.User._Email == email).Key != null)
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

        public List<Home.Home> GetHome(string sessionKey)
        {
            return _Sessions[sessionKey].GetHomes();
        }
    }
}
