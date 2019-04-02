using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Home
{
    public class BasicHomeFactory:AbstractHomeFactory
    {
        DatabaseConnection _DatabaseConnection;

        public BasicHomeFactory()
        {
            _DatabaseConnection = new DatabaseConnection();
            _AdressBuilder = new AdressBuilder();
            _OwnerBuilder = new ConcreteOwnerBuilder();
        }

        //TYLKO DO TESTOW
        public Home TestHome()
        {
            Home home = new Flat
            {
                _ID = 8902
            };

            _AdressBuilder.AddCity("City");
            _AdressBuilder.AddStreetName("Street");
            _AdressBuilder.AddHouseNumber(22);
            _AdressBuilder.AddFlatNumber(11);
            _AdressBuilder.AddPostalCode("11-222");
            home._Adress = _AdressBuilder.BuildAdress();


            _ContractBuilder = new BasicContractBuilder();
            _ContractBuilder.AddID(123);
            _ContractBuilder.AddSigningDate(DateTime.Now);
            _ContractBuilder.AddEndDate(DateTime.Now);
            _ContractBuilder.AddFtpAdress("c://wspolnota_mieszkaniowa");
            home._Contracts.Add(_ContractBuilder.BuildContract());

            _ContractBuilder = new BasicContractBuilder();
            _ContractBuilder.AddID(456);
            _ContractBuilder.AddSigningDate(DateTime.Now);
            _ContractBuilder.AddEndDate(DateTime.Now);
            _ContractBuilder.AddFtpAdress("c://dostawcy_mediow");
            
            _OwnerBuilder.AddFirstName("FirstName");
            _OwnerBuilder.AddSecondName("Secondname");
            _OwnerBuilder.AddID(999);
            _OwnerBuilder.AddEmail("em@i.l");
            home._Owner = _OwnerBuilder.BuildOwner();

            _CountersBuilder = new BasicCounterBuilder();
            _CountersBuilder.AddID(112233);
            _CountersBuilder.AddType("electricity");
            _CountersBuilder.AddProvider(new BasicProvider() { ID = 12, _Name = "Energa", _Adress = _AdressBuilder.BuildAdress(), _Contract = _ContractBuilder.BuildContract() });
            _CountersBuilder.AddFee(new BasicFee() { ID = 44, _Currency = "PLN", _Name = "Transportowa", _PricePerUnit = 12.22M, _Unit = "MWH" });
            _CountersBuilder.AddReading(DateTime.Now.AddDays(-5), new Reading() { ID = 77, _Date = DateTime.Now.AddDays(-5), _State = 10 });
            _CountersBuilder.AddReading(DateTime.Now.AddDays(-3), new Reading() { ID = 78, _Date = DateTime.Now.AddDays(-3), _State = 20 });
            home._Counters.Add(_CountersBuilder.BuildCounter());

            _CountersBuilder = new BasicCounterBuilder();
            _CountersBuilder.AddID(223344);
            _CountersBuilder.AddType("gas");
            _CountersBuilder.AddProvider(new BasicProvider() { ID = 13, _Name = "PGNiG", _Adress = _AdressBuilder.BuildAdress(), _Contract = _ContractBuilder.BuildContract() });
            _CountersBuilder.AddFee(new BasicFee() { ID = 44, _Currency = "PLN", _Name = "Transportowa", _PricePerUnit = 1.55M, _Unit = "m3" });
            _CountersBuilder.AddReading(DateTime.Now.AddDays(-2), new Reading() { ID = 79, _Date = DateTime.Now.AddDays(-2), _State = 15 });
            _CountersBuilder.AddReading(DateTime.Now.AddDays(-1), new Reading() { ID = 80, _Date = DateTime.Now.AddDays(-1), _State = 26 });
            home._Counters.Add(_CountersBuilder.BuildCounter());

            return home;
        }

        public override Home CreateNewHome(string json)
        {
            throw new NotImplementedException();
        }

        public override Home CreateHomeFromDataBase(string homeName)
        {
            var flatDataFromDB = _DatabaseConnection.GetHomeData(homeName);

            Home home = new Flat
            {
                _ID = Int32.Parse( flatDataFromDB["Name"])
            };

            _AdressBuilder.AddCity(flatDataFromDB["City"]);
            _AdressBuilder.AddStreetName(flatDataFromDB["Street"]);
            _AdressBuilder.AddHouseNumber(Int32.Parse( flatDataFromDB["HouseNumber"]));
            _AdressBuilder.AddFlatNumber(Int32.Parse(flatDataFromDB["FlatNumber"]));
            _AdressBuilder.AddPostalCode(flatDataFromDB["PostalCode"]);

            home._Adress = _AdressBuilder.BuildAdress();

            string[] contracts = flatDataFromDB["Contracts"].Split("|", StringSplitOptions.RemoveEmptyEntries);
            foreach(string c in contracts)
            {
                var contract = _DatabaseConnection.GetContractsData(c);
                _ContractBuilder = new BasicContractBuilder();
                _ContractBuilder.AddID(Int32.Parse( contract["Name"]));
                _ContractBuilder.AddSigningDate(DateTime.Parse( contract["Signing"]));
                _ContractBuilder.AddEndDate(DateTime.Parse(contract["Expiration"]));
                _ContractBuilder.AddFtpAdress(contract["Ftp"]);
                home._Contracts.Add(_ContractBuilder.BuildContract());
            }

            string[] counters = flatDataFromDB["Counters"].Split("|", StringSplitOptions.RemoveEmptyEntries);
            foreach(string c in counters)
            {
                var counter = _DatabaseConnection.GetCountersData(c);
                _CountersBuilder = new BasicCounterBuilder();
                _CountersBuilder.AddID(Int32.Parse(counter["Counters.Name"]));
                _CountersBuilder.AddType(counter["Counters.Type"]);
                //DO UZUPELNIENIA!!!!
                home._Counters.Add(_CountersBuilder.BuildCounter());
            }
            
            var ownerDataFromDB = _DatabaseConnection.GetOwnerData(flatDataFromDB["Owner"]);
            _OwnerBuilder.AddFirstName(ownerDataFromDB["FirstName"]);
            _OwnerBuilder.AddSecondName(ownerDataFromDB["Secondname"]);
            _OwnerBuilder.AddID(Int32.Parse(ownerDataFromDB["IdNumber"]));
            _OwnerBuilder.AddEmail(ownerDataFromDB["Email"]);

            home._Owner = _OwnerBuilder.BuildOwner();

            return home;
        }
    }
}
