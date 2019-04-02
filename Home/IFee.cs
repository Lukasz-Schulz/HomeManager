namespace Home
{
    public interface IFee
    {
        int ID { get; set; }
        string _Name { get; set; }
        string _Unit { get; set; }
        decimal _PricePerUnit { get; set; }
        string _Currency { get; set; }
    }
}