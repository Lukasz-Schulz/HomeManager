namespace Home
{
    public interface IOwner
    {
        int ID { get; set; }
        string _FirstName { get; set; }
        string _SecondName { get; set; }
        string _Email { get; set; }
    }

    public class BasicOwner : IOwner
    {
        public int ID { get; set; }
        public string _FirstName { get; set; }
        public string _SecondName { get; set; }
        public string _Email { get; set; }
    }
}