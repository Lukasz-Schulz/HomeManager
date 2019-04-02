using System;

namespace Home
{
    public interface IContract
    {
        int ID { get; set; }
        DateTime _SigningDate { get; set; }
        DateTime _EndDate { get; set; }
        string _FtpAdress { get; set; }
    }
}