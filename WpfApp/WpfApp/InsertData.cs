using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WpfApp
{
    class InsertData
    {
        /// <summary>
        /// method that inserts a person object from database
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="name"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static string InsertPerson(int personID, string name, string city, string state, string country)
        {
            string connectionString = "Server=localhost;Database=kabsu; User ID = appuser; Password = test; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var insertCommand = new MySqlCommand("kabsu.InsertPerson", connection))
                    {
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        insertCommand.Parameters.AddWithValue("@PersonID", personID);
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@City", city);
                        insertCommand.Parameters.AddWithValue("@State", state);
                        insertCommand.Parameters.AddWithValue("@Country", country);

                        connection.Open();

                        var reader = insertCommand.ExecuteReader();

                        reader.Read();
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void InsertPerson(string name, string city, string state, string country)
        {
            string connectionString = "Server=localhost;Database=kabsu; User ID = appuser; Password = test; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var insertCommand = new MySqlCommand("kabsu.InsertPerson", connection))
                    {
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@City", city);
                        insertCommand.Parameters.AddWithValue("@State", state);
                        insertCommand.Parameters.AddWithValue("@Country", country);

                        connection.Open();

                        var reader = insertCommand.ExecuteReader();

                        while (reader.Read())
                        {

                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }
        
        public static string InsertAnimal(string animalID, string name, string breed, string species, string regNum)
        {
            string connectionString = "Server=localhost;Database=kabsu; User ID = appuser; Password = test; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand("kabsu.InsertAnimal", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AnimalID", animalID);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Breed", breed);
                        command.Parameters.AddWithValue("@Species", species);
                        command.Parameters.AddWithValue("@RegNum", regNum);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        reader.Read();
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string InsertSample(string valid, string canNum, string code, string collectionDate, int numUnits, string notes, string personName, string city, string state)
        {
            string connectionString = "Server=localhost;Database=kabsu; User ID = appuser; Password = test; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand("kabsu.InsertSample", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Valid", valid);
                        command.Parameters.AddWithValue("@CanNum", canNum);
                        command.Parameters.AddWithValue("@Code", code);
                        command.Parameters.AddWithValue("@CollDate", collectionDate);
                        command.Parameters.AddWithValue("@NumUnits", numUnits);
                        command.Parameters.AddWithValue("@Notes", notes);
                        command.Parameters.AddWithValue("@PersonName", personName);
                        command.Parameters.AddWithValue("@City", city);
                        command.Parameters.AddWithValue("@State", state);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        reader.Read();
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
