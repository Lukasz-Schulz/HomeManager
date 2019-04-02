namespace Home
{
    public interface IProvider
    {
        int ID { get; set; }
        string _Name { get; set; }
        Adress _Adress { get; set; }
        IContract _Contract { get; set; }
    }
}