using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home
{
    public class DatabaseConnection
    {
        readonly string ConnectionString = @"Server=tcp:homemanager.database.windows.net,1433;Initial Catalog=Homemanager;Persist Security Info=False;User ID=Bwana182;Password=Auniha182;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=10;";

        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private SqlDataReader _dataReader;

        //change to private later
        public bool CheckIfTableExists(string tableName)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlCommand = new SqlCommand($"SELECT COUNT(*) FROM {tableName}", _sqlConnection);

                try
                {
                    _sqlCommand.Connection.Open();
                    _sqlCommand.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    _sqlCommand.Connection.Close();
                }
            }
        }

        private void CreateTable(string query)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlCommand = new SqlCommand(query, _sqlConnection);
                _sqlCommand.Connection.Open();
                _sqlCommand.ExecuteNonQuery();
                _sqlCommand.Connection.Close();
            }
        }

        //change to private
        public void CreateHomesTable()
        {
            string query = @"create table Homes(
                Name varchar(30),
                Counters varchar(300),
                Owner int,
                Contracts varchar(500),
                Street varchar(50),
                HouseNumber int,
                FlatNumber int,
                PostalCode varchar(8),
                City varchar(50))";

            if (!CheckIfTableExists("Homes"))
            {
                CreateTable(query);
            }
        }

        public void CreateCountersTable()
        {
            string query = @"create table Counters(
                Name varchar(45) PRIMARY KEY,
                Provider int,
                Type varchar(45),
                Readings varchar(1500),
                Fees varchar(500))";

            if (!CheckIfTableExists("Counters"))
            {
                CreateTable(query);
            }
        }

        public void CreateOwnersTable()
        {
            string query = @"create table Owners(
                IdNumber varchar(45) PRIMARY KEY,
                FirstName varchar(300),
                SecondName varchar(300),
                Email varchar(300))";

            if (!CheckIfTableExists("Owners"))
            {
                CreateTable(query);
            }
        }

        public void CreateProvidersTable()
        {
            string query = @"create table Providers(
                IdNumber varchar(45) PRIMARY KEY,
                Name varchar(100),   
                Contract varchar(50),
                Street varchar(50),
                HouseNumber int,
                FlatNumber int,
                PostalCode varchar(8),
                City varchar(50))";

            if (!CheckIfTableExists("Providers"))
            {
                CreateTable(query);
            }
        }

        public void CreateContractsTable()
        {
            string query = @"create table Contracts(
                Name varchar(45) PRIMARY KEY,
                Signing varchar(20),
                Expiration varchar(20),
                Ftp varchar(100))";

            if (!CheckIfTableExists("Contracts"))
            {
                CreateTable(query);
            }
        }

        public Dictionary<string, string> GetHomeData(string homename)
        {
            Dictionary<string, string> sqlResponse = new Dictionary<string, string>();
            string query = @"SELECT Name, Counters, Owner, Street, HouseNumber, FlatNumber, PostalCode, City, Contracts FROM Homes WHERE Name=@param";
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlCommand = new SqlCommand(query, _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("@param", homename);
                try
                {
                    _sqlCommand.Connection.Open();
                    _dataReader = _sqlCommand.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        sqlResponse.Add(_dataReader.GetName(0), _dataReader.GetSqlValue(0).ToString());
                        sqlResponse.Add(_dataReader.GetName(1), _dataReader.GetString(1));
                        sqlResponse.Add(_dataReader.GetName(2), _dataReader.GetSqlValue(2).ToString());
                        sqlResponse.Add(_dataReader.GetName(3), _dataReader.GetSqlValue(3).ToString());
                        sqlResponse.Add(_dataReader.GetName(4), _dataReader.GetSqlValue(4).ToString());
                        sqlResponse.Add(_dataReader.GetName(5), _dataReader.GetSqlValue(5).ToString());
                        sqlResponse.Add(_dataReader.GetName(6), _dataReader.GetString(6));
                        sqlResponse.Add(_dataReader.GetName(7), _dataReader.GetString(7));
                        sqlResponse.Add(_dataReader.GetName(8), _dataReader.GetString(8));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    _sqlCommand.Connection.Close();
                }
            }
            return sqlResponse;
        }

        public Dictionary<string, string> GetContractsData(string contractId)
        {
            Dictionary<string, string> sqlResponse = new Dictionary<string, string>();
            string query = @"SELECT Name, Signing, Expiration, Ftp from Contracts WHERE Name=@param";
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlCommand = new SqlCommand(query, _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("@param", contractId);
                try
                {
                    _sqlCommand.Connection.Open();
                    _dataReader = _sqlCommand.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        sqlResponse.Add(_dataReader.GetName(0), _dataReader.GetValue(0).ToString());
                        sqlResponse.Add(_dataReader.GetName(1), _dataReader.GetValue(1).ToString());
                        sqlResponse.Add(_dataReader.GetName(2), _dataReader.GetValue(2).ToString());
                        sqlResponse.Add(_dataReader.GetName(3), _dataReader.GetValue(3).ToString());
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    _sqlCommand.Connection.Close();
                }
            }
            return sqlResponse;
        }

        public Dictionary<string, string> GetOwnerData(string ownerId)
        {
            Dictionary<string, string> sqlResponse = new Dictionary<string, string>();
            string query = @"SELECT IdNumber, FirstName, Secondname, Email FROM Owners WHERE IdNumber=@param";
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlCommand = new SqlCommand(query, _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("@param", ownerId);
                try
                {
                    _sqlCommand.Connection.Open();
                    _dataReader = _sqlCommand.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        sqlResponse.Add(_dataReader.GetName(0), _dataReader.GetSqlValue(0).ToString());
                        sqlResponse.Add(_dataReader.GetName(1), _dataReader.GetSqlValue(1).ToString());
                        sqlResponse.Add(_dataReader.GetName(2), _dataReader.GetSqlValue(2).ToString());
                        sqlResponse.Add(_dataReader.GetName(3), _dataReader.GetSqlValue(3).ToString());
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    _sqlCommand.Connection.Close();
                }
            }
            return sqlResponse;
        }

        public Dictionary<string, string> GetCountersData(string counterId)
        {
            Dictionary<string, string> sqlResponse = new Dictionary<string, string>();
            string query = @"SELECT Counters.Name, Counters.Type, Counters.Readings, Counters.Fees, 
                            Providers.Name, Providers.Contract, Providers.Street, Providers.HouseNumber, Providers.FlatNumber, Providers.PostalCode, Providers.City,
                            Contracts.Signing, Contracts.Expiration, Contracts.Ftp
                            FROM Counters 
                            LEFT JOIN Providers ON(Counters.Provider = Providers.IdNumber) 
                            LEFT JOIN Contracts ON(Providers.Contract = Contracts.Name)
                            WHERE Counters.Name=@param";
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                _sqlCommand = new SqlCommand(query, _sqlConnection);
                _sqlCommand.Parameters.AddWithValue("@param", counterId);
                try
                {
                    _sqlCommand.Connection.Open();
                    _dataReader = _sqlCommand.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        sqlResponse.Add("Counters."+_dataReader.GetName(0), _dataReader.GetValue(0).ToString());
                        sqlResponse.Add("Counters." + _dataReader.GetName(1), _dataReader.GetValue(1).ToString());
                        sqlResponse.Add("Counters." + _dataReader.GetName(2), _dataReader.GetValue(2).ToString());
                        sqlResponse.Add("Counters." + _dataReader.GetName(3), _dataReader.GetValue(3).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(4), _dataReader.GetValue(4).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(5), _dataReader.GetValue(5).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(6), _dataReader.GetValue(6).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(7), _dataReader.GetValue(7).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(8), _dataReader.GetValue(8).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(9), _dataReader.GetValue(9).ToString());
                        sqlResponse.Add("Providers." + _dataReader.GetName(10), _dataReader.GetValue(10).ToString());
                        sqlResponse.Add("Contracts." + _dataReader.GetName(11), _dataReader.GetValue(11).ToString());
                        sqlResponse.Add("Contracts." + _dataReader.GetName(12), _dataReader.GetValue(12).ToString());
                        sqlResponse.Add("Contracts." + _dataReader.GetName(13), _dataReader.GetValue(13).ToString());
                    }
                }
                catch (Exception ex)
                {
                    var exc = ex;
                    return null;
                }
                finally
                {
                    _sqlCommand.Connection.Close();
                }
            }
            return sqlResponse;
        }

        //public Dictionary<string, string> GetMediaMeters(string ownerName)
        //{
        //    Dictionary<string, string> sqlResponse = new Dictionary<string, string>();
        //    string query = @"SELECT Phone_number, Email, City, Postal_code, Street, Number FROM Companies WHERE Name=@param
        //        LIMIT 1";
        //    _sqlCommand = new SqlCommand(query, _sqlConnection);
        //    _sqlCommand.Parameters.AddWithValue("@param", companyName);
        //    try
        //    {
        //        _sqlCommand.Connection.Open();
        //        _dataReader = _sqlCommand.ExecuteReader();

        //        while (_dataReader.Read())
        //        {
        //            sqlResponse.Add(_dataReader.GetName(0), _dataReader.GetSqlValue(0).ToString());
        //            sqlResponse.Add(_dataReader.GetName(1), _dataReader.GetString(1));
        //            sqlResponse.Add(_dataReader.GetName(2), _dataReader.GetString(2));
        //            sqlResponse.Add(_dataReader.GetName(3), _dataReader.GetString(3));
        //            sqlResponse.Add(_dataReader.GetName(4), _dataReader.GetString(4));
        //            sqlResponse.Add(_dataReader.GetName(5), _dataReader.GetString(5));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        _sqlCommand.Connection.Close();
        //    }
        //    return sqlResponse;
        //}




        public void AddHomeToDatabase(Home home)
        {
            StringBuilder contracts = new StringBuilder();
            foreach (var c in home._Contracts)
            {
                contracts.Append(c.ID + "|");
            }

            StringBuilder counters = new StringBuilder();
            foreach (var c in home._Counters)
            {
                counters.Append(c.ID + "|");
            }

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                string insertString = "Insert into Homes(Name,Counters,Owner,Contracts,Street,HouseNumber,FlatNumber,PostalCode,City) " +
                    "values(@name,@counters,@owner,@contracts,@street,@houseNo,@flatNo,@postalCode,@city)";

                SqlCommand _sqlCommandInsert = new SqlCommand(insertString, _sqlConnection);
                _sqlCommandInsert.Parameters.AddWithValue("@name", home._ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@counters", counters.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@owner", home._Owner.ID);
                _sqlCommandInsert.Parameters.AddWithValue("@contracts", contracts.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@street", home._Adress._StreetName);
                _sqlCommandInsert.Parameters.AddWithValue("@houseNo", home._Adress._HouseNumber);
                _sqlCommandInsert.Parameters.AddWithValue("@flatNo", home._Adress._FlatNumber);
                _sqlCommandInsert.Parameters.AddWithValue("@postalCode", home._Adress._PostalCode);
                _sqlCommandInsert.Parameters.AddWithValue("@city", home._Adress._City);

                _sqlCommandInsert.Connection.Open();
                _sqlCommandInsert.ExecuteNonQuery();

                _sqlCommandInsert.Connection.Close();
            }
        }

        public void AddOwnerToDatabase(IOwner owner)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                string insertString = "Insert into Owners(IdNumber,FirstName,SecondName,Email) " +
                    "values(@IdNumber,@FirstName,@SecondName,@Email)";

                SqlCommand _sqlCommandInsert = new SqlCommand(insertString, _sqlConnection);
                _sqlCommandInsert.Parameters.AddWithValue("@IdNumber", owner.ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@FirstName", owner._FirstName.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@SecondName", owner._SecondName.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Email", owner._Email.ToString());

                _sqlCommandInsert.Connection.Open();
                _sqlCommandInsert.ExecuteNonQuery();

                _sqlCommandInsert.Connection.Close();
            }
        }

        public void AddCounterToDatabase(ICounter counter)
        {
            StringBuilder readings = new StringBuilder();
            foreach(var reading in counter._Readings){
                readings.Append(reading.Value._Date + "%" + reading.Value.ID + "%" + reading.Value._State + "%" + "|");
            }

            StringBuilder fees = new StringBuilder();
            foreach(var fee in counter._Fees){
                readings.Append(fee.ID + "%" + fee._Name + "%" + fee._Unit + "%" + fee._PricePerUnit + "%" + fee._Currency + "|");
            }

            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                string insertString = "Insert into Counters(Name, Provider, Type, Readings, Fees) " +
                    "values(@Name,@Provider,@Type,@Readings, @Fees)";


                SqlCommand _sqlCommandInsert = new SqlCommand(insertString, _sqlConnection);
                _sqlCommandInsert.Parameters.AddWithValue("@Name", counter.ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Provider", counter._Provider.ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Type", counter._Type.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Readings", readings.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Fees", fees.ToString());

                _sqlCommandInsert.Connection.Open();
                _sqlCommandInsert.ExecuteNonQuery();

                _sqlCommandInsert.Connection.Close();
            }
        }

        public void AddProviderToDatabase(IProvider provider)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                string insertString = "Insert into Providers(IdNumber, Name, Contract,Street,HouseNumber, FlatNumber, PostalCode, City) " +
                    "values(@IdNumber, @Name,@Contract,@Street,@HouseNumber, @FlatNumber, @PostalCode, @City)";

                SqlCommand _sqlCommandInsert = new SqlCommand(insertString, _sqlConnection);
                _sqlCommandInsert.Parameters.AddWithValue("@IdNumber", provider.ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Name", provider._Name.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Contract", provider._Contract.ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@Street", provider._Adress._StreetName.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@HouseNumber", provider._Adress._HouseNumber.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@FlatNumber", provider._Adress._FlatNumber.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@PostalCode", provider._Adress._PostalCode.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@City", provider._Adress._City.ToString());

                _sqlCommandInsert.Connection.Open();
                _sqlCommandInsert.ExecuteNonQuery();
                _sqlCommandInsert.Connection.Close();
            }
        }

        public void AddContractToDatabase(IContract contract)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                string insertString = "Insert into Contracts(Name, Signing, Expiration, Ftp) " +
                    "values(@Name, @Signing, @Expiration, @Ftp)";

                SqlCommand _sqlCommandInsert = new SqlCommand(insertString, _sqlConnection);
                _sqlCommandInsert.Parameters.AddWithValue("@Name", contract.ID.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@signing", contract._SigningDate.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@expiration", contract._EndDate.ToString());
                _sqlCommandInsert.Parameters.AddWithValue("@ftp", contract._EndDate.ToString());

                _sqlCommandInsert.Connection.Open();
                _sqlCommandInsert.ExecuteNonQuery();

                _sqlCommandInsert.Connection.Close();
            }
        }
    }
}
