namespace Home
{
    public class BasicOwner : IOwner
    {
        public int ID { get; set; }
        public string _FirstName { get; set; }
        public string _SecondName { get; set; }
        public string _Email { get; set; }
    }
}