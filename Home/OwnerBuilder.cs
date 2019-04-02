namespace Home
{
    public class ConcreteOwnerBuilder : IOwnerBuilder
    {
        private IOwner _Owner = new BasicOwner();

        public void AddID(int id)
        {
            _Owner.ID = id;
        }

        public void AddFirstName(string name)
        {
            _Owner._FirstName = name;
        }

        public void AddSecondName(string name)
        {
            _Owner._SecondName = name;
        }

        public void AddEmail(string email)
        {
            _Owner._Email = email;
        }

        public IOwner BuildOwner()
        {
            return _Owner;
        }
    }
}