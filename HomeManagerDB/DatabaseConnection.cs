using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManagerDB
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

        private void CreateCountersTable()
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

        private void CreateOwnersTable()
        {
            string query = @"create table Owners(
                Provider_name varchar(45) PRIMARY KEY,
                Price_components varchar(300),
                Company_name varchar(300)";

            if (!CheckIfTableExists("Owners"))
            {
                CreateTable(query);
            }
        }

        private void CreateProvidersTable()
        {
            string query = @"create table Adresses(
                Name varchar(100) PRIMARY KEY,   
                Contract varchar(50),
                Street varchar(50),
                HouseNumber int,
                FlatNumber int,
                PostalCode varchar(8),
                City varchar(50))";

            if (!CheckIfTableExists("Adresses"))
            {
                CreateTable(query);
            }
        }

        private void CreateContractsTable()
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
            string query = @"SELECT Name, Counters, Owner, Adress, Contracts FROM Homes WHERE Name=@param
                LIMIT 1";
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
                    sqlResponse.Add(_dataReader.GetName(2), _dataReader.GetString(2));
                    sqlResponse.Add(_dataReader.GetName(3), _dataReader.GetString(3));
                    sqlResponse.Add(_dataReader.GetName(4), _dataReader.GetString(4));
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




        public void TEST(Home home)
        {
            using (_sqlConnection = new SqlConnection(ConnectionString))
            {
                string insertString = "Insert into Companies(Name,Counters,Owner,Contracts,Street,HouseNumber,FlatNumber,PostalCode,City) " +
                    "values(@name,@counters,@owner,@contracts,@street,@houseNo,@flatNo,@postalCode,@city)";

                SqlCommand _sqlCommandInsert = new SqlCommand(insertString, _sqlConnection);
                _sqlCommandInsert.Parameters.AddWithValue("@name", "a");
                _sqlCommandInsert.Parameters.AddWithValue("@counters", "1");
                _sqlCommandInsert.Parameters.AddWithValue("@owner", "b");
                _sqlCommandInsert.Parameters.AddWithValue("@contracts", "a");
                _sqlCommandInsert.Parameters.AddWithValue("@street", "b");
                _sqlCommandInsert.Parameters.AddWithValue("@houseNo", "z");
                _sqlCommandInsert.Parameters.AddWithValue("@flatNo", "x");
                _sqlCommandInsert.Parameters.AddWithValue("@postalCode", "x");
                _sqlCommandInsert.Parameters.AddWithValue("@city", "x");

                _sqlCommand.Connection.Open();
                _sqlCommandInsert.ExecuteNonQuery();
                SqlDataReader reader = _sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var i = reader.GetString(0);
                    var j = reader.GetInt32(1);
                    var k = reader.GetString(2);
                    var l = reader.GetString(3);
                    var hg = reader.GetString(4);
                }
                _sqlCommand.Connection.Close();
            }
        }
    }
}
