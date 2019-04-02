namespace Home
{
    public class AdressBuilder
    {
        private Adress _Adress = new Adress();
        
        public void AddStreetName(string name)
        {
            _Adress._StreetName = name;
        }

        public void AddHouseNumber(int number)
        {
            _Adress._HouseNumber = number;
        }

        public void AddFlatNumber(int number)
        {
            _Adress._FlatNumber = number;
        }

        public void AddCity(string city)
        {
            _Adress._City = city;
        }

        public void AddPostalCode(string code)
        {
            _Adress._PostalCode = code;
        }

        public Adress BuildAdress()
        {
            return _Adress;
        }
    }
}