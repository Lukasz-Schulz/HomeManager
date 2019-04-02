using System;
using System.Collections.Generic;

namespace Home
{
    public class Counter : ICounter
    {
        public int ID { get; set; }
        public IProvider _Provider { get; set; }
        public string _Type { get; set; }
        public Dictionary<DateTime, Reading> _Readings { get; set; }
        public List<IFee> _Fees { get; set; }
    }
}