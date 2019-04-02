using System;

namespace Home
{
    public class BasicCounterBuilder : ICounterBuilder
    {
        private ICounter _Counter = new Counter();

        public void AddID(int ID)
        {
            _Counter.ID = ID;
        }
        public void AddFee(IFee fee)
        {
            if(_Counter._Fees == null)
            {
                _Counter._Fees = new System.Collections.Generic.List<IFee>();
            }
            _Counter._Fees.Add(fee);
        }

        public void AddProvider(IProvider provider)
        {
            _Counter._Provider = provider;
        }

        public void AddReading(DateTime date, Reading reading)
        {
            if(_Counter._Readings == null)
            {
                _Counter._Readings = new System.Collections.Generic.Dictionary<DateTime, Reading>();
            }
            _Counter._Readings.Add(date, reading);
        }

        public void AddType(string type)
        {
            _Counter._Type = type;
        }

        public ICounter BuildCounter()
        {
            //if (_Counter._Type!=null & _Counter._Fees!=null & _Counter._Provider!=null & _Counter.ID!=0 & _Counter._Readings!=null)
            //{
                return _Counter;
            //}
            //else
            //{
            //    throw new Exception("This counter object is not complete.");
            //}
        }
    }
}