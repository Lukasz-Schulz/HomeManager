using System;

namespace Home
{
    public class Contract : IContract
    {
        public int ID { get; set; }
        public DateTime _SigningDate { get; set; }
        public DateTime _EndDate { get; set; }
        public string _FtpAdress { get; set; }
    }
}