using System;
using System.Collections.Generic;
using System.Text;

namespace HomeManagerSecurity
{
    public class Session
    {
        public string User { get; private set; }
        public DateTime Timeout { get; private set; }
        public bool IsOn { get; private set; }
        public string SessionId { get; private set; }

        public Session(string user)
        {
            User = user;
            Timeout = DateTime.Now.AddMinutes(5);
            IsOn = true;
            SessionId = user + DateTime.Now.ToString();
        }

        public bool CheckSession()
        {
            if (DateTime.Now <= Timeout && IsOn)
            {
                Timeout = DateTime.Now.AddMinutes(5);
                return IsOn;
            }
            else
            {
                IsOn = false;
                return IsOn;
            }
        }

        public void DestroySession()
        {
            IsOn = false;
        }
    }
}
