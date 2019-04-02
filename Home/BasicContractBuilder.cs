using System;

namespace Home
{
    public class BasicContractBuilder : IContractBuilder
    {
        private IContract _Contract = new Contract();

        public void AddEndDate(DateTime date)
        {
            _Contract._EndDate = date;
        }

        public void AddFtpAdress(string ftp)
        {
            _Contract._FtpAdress = ftp;
        }

        public void AddID(int id)
        {
            _Contract.ID = id;
        }

        public void AddSigningDate(DateTime date)
        {
            _Contract._SigningDate = date;
        }

        public IContract BuildContract()
        {
            return _Contract;
        }
    }
}
