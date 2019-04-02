namespace Home
{
    public class BasicProvider : IProvider
    {
        public int ID { get; set; }
        public string _Name { get; set; }
        public Adress _Adress { get; set; }
        public IContract _Contract { get; set; }
    }
}