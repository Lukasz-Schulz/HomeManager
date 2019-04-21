namespace Home
{
    public interface IOwner
    {
        int ID { get; set; }
        string _FirstName { get; set; }
        string _SecondName { get; set; }
        string _Email { get; set; }
    }
}