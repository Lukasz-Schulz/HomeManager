namespace Home
{
    public interface IOwnerBuilder
    {
        void AddEmail(string email);
        void AddFirstName(string name);
        void AddID(int id);
        void AddSecondName(string name);
        IOwner BuildOwner();
    }
}