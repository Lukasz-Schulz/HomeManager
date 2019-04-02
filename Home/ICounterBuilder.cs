using System;

namespace Home
{
    public interface ICounterBuilder
    {
        void AddID(int ID);
        void AddProvider(IProvider provider);
        void AddType(string type);
        void AddReading(DateTime date, Reading reading);
        void AddFee(IFee fee);
        ICounter BuildCounter();
    }
}