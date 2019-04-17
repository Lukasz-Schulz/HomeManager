using System.Collections.Generic;
using System.Text;

namespace Home
{
    public abstract class AbstractHomeFactory
    {
        protected AdressBuilder _AdressBuilder { get; set; } 
        protected ICounterBuilder _CountersBuilder { get; set; } 
        protected IOwnerBuilder _OwnerBuilder { get; set; }
        protected IContractBuilder _ContractBuilder { get; set; }

        public abstract List<Home> CreateHomeFromDataBase(string homeName);
        public abstract Home CreateNewHome(string json);
    }
}
