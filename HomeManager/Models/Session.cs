using Home;
using System;
using System.Collections.Generic;

namespace HomeManager.Models
{
    public partial class SessionHolder
    {
        protected class Session
        {
            private DatabaseConnection databaseConnection = new DatabaseConnection();
            public DateTime Start { get; private set; }
            public DateTime Expiry { get; private set; }
            public Home.IOwner User { get; private set; }
            public string SessionKey { get; private set; }
            public Session(string email)
            {
                var user = databaseConnection.GetOwnerDataByEmail(email);
                ConcreteOwnerBuilder concreteOwnerBuilder = new ConcreteOwnerBuilder();
                concreteOwnerBuilder.AddFirstName(user["FirstName"]);
                concreteOwnerBuilder.AddSecondName(user["Secondname"]);
                concreteOwnerBuilder.AddEmail(user["Email"]);
                concreteOwnerBuilder.AddID(int.Parse(user["IdNumber"]));
                User = concreteOwnerBuilder.BuildOwner();
                Start = DateTime.Now;
                ProlongueSession();
                var random = new Random();
                SessionKey = random.Next(10, 99).ToString()
                    + (char)('a' + random.Next(1, 26))
                    + (char)('a' + random.Next(1, 26))
                    + random.Next(101, 999)
                    +(char)('a' + random.Next(1, 26))
                    + (char)('a' + random.Next(1, 26))
                    + random.Next(10, 99);
            }

            public void ProlongueSession()
            {
                Expiry = DateTime.Now.AddMinutes(5);
            }

            public List<Home.Home> GetHomes()
            {
                BasicHomeFactory homeFactory = new BasicHomeFactory();
                var home = homeFactory.CreateHomeFromDataBase(User.ID.ToString());
                return home;
            }
        }
    }
}
