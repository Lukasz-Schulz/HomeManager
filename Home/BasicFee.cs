namespace Home
{
    public class BasicFee : IFee
    {
        public int ID { get; set; }
        public string _Name { get; set; }
        public string _Unit { get; set; }
        public decimal _PricePerUnit { get; set; }
        public string _Currency { get; set; }
    }
}