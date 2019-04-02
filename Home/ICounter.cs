using System;
using System.Collections.Generic;

namespace Home
{
    public interface ICounter
    {
        int ID { get; set; }
        IProvider _Provider { get; set; }
        string _Type { get; set; }
        Dictionary<DateTime, Reading> _Readings { get; set; }
        List<IFee> _Fees { get; set; }
    }
}