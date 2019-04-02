using System;
using System.Collections.Generic;

namespace Home
{
    public abstract class Home
    {
        public Adress _Adress { get; set; }
        public List<ICounter> _Counters { get; set; } = new List<ICounter>();
        public IOwner _Owner { get; set; }
        public List<IContract> _Contracts { get; set; } = new List<IContract>();
        public int _ID { get; set; }
    }
}
