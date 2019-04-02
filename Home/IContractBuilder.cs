using System;

namespace Home
{
    public interface IContractBuilder
    {
        void AddID(int id);
        void AddSigningDate(DateTime date);
        void AddEndDate(DateTime date);
        void AddFtpAdress(string ftp);
        IContract BuildContract();
    }
}