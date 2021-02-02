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
    /// <summary>
    /// class that adds or edits records from multiple search results
    /// </summary>
    public class SearchResults
    {
        public SearchResult searchResult
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Connects to the SQL Database and searches for the SearchTerm st and returns a list of results
        /// </summary>
        /// <param name="st">An instance of SearchTerm containing the different search parameters</param>
        /// <returns>A list containing the results of the search</returns>
        public List<SearchResult> retrieveData(SearchTerm st)
        {
            string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand("kabsu.RetrieveData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    

                        command.Parameters.AddWithValue("@Owner", st.Owner);
                        command.Parameters.AddWithValue("@Breed", st.Breed);
                        command.Parameters.AddWithValue("@AnimalName", st.AnimalName);
                        command.Parameters.AddWithValue("@Code", st.Code);
                        command.Parameters.AddWithValue("@CanNum", st.CanNum);
                        command.Parameters.AddWithValue("@Town", st.Town);
                        command.Parameters.AddWithValue("@State", st.State);
                        connection.Open();

                        var reader = command.ExecuteReader();

                        var resultList = new List<SearchResult>();

                        while (reader.Read())
                        {
                            searchResult= new SearchResult(
                               reader.GetString(reader.GetOrdinal("LastModified")),
                               reader.GetString(reader.GetOrdinal("CanNum")),
                               reader.GetString(reader.GetOrdinal("AnimalID")),
                               reader.GetString(reader.GetOrdinal("CollDate")),
                               reader.GetString(reader.GetOrdinal("NumUnits")),
                               reader.GetString(reader.GetOrdinal("AnimalName")),
                               reader.GetString(reader.GetOrdinal("Breed")),
                               reader.GetString(reader.GetOrdinal("RegNum")),
                               reader.GetString(reader.GetOrdinal("PersonName")),
                               reader.GetString(reader.GetOrdinal("City")),
                               reader.GetString(reader.GetOrdinal("State")),
                               reader.GetString(reader.GetOrdinal("Country")),
                               reader.GetString(reader.GetOrdinal("Species")));
                            resultList.Add(searchResult);
                        }
                        connection.Close();
                        return resultList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database.");
                return new List<SearchResult>();
            }
        }

        /// <summary>
        /// this does exactly the same as above, but the canNum has a range. 
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public List<SearchResult> retrieveDataBetween(SearchTerm st, string upperCanNumber)
        {

            string connectionString = "Server=mysql.cs.ksu.edu;Database=kabsu; User ID = kabsu; Password = insecurepassword; Integrated Security=true";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    using (var command = new MySqlCommand("kabsu.RetrieveDataBetween", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@Owner", st.Owner);
                        command.Parameters.AddWithValue("@Breed", st.Breed);
                        command.Parameters.AddWithValue("@AnimalName", st.AnimalName);
                        command.Parameters.AddWithValue("@Code", st.Code);
                        command.Parameters.AddWithValue("@CanNum", st.CanNum);
                        command.Parameters.AddWithValue("@UpperCanNum", upperCanNumber);
                        command.Parameters.AddWithValue("@Town", st.Town);
                        command.Parameters.AddWithValue("@State", st.State);
                        connection.Open();

                        var reader = command.ExecuteReader();

                        var resultList = new List<SearchResult>();

                        while (reader.Read())
                        {
                            searchResult = new SearchResult(
                               reader.GetString(reader.GetOrdinal("LastModified")),
                               reader.GetString(reader.GetOrdinal("CanNum")),
                               reader.GetString(reader.GetOrdinal("AnimalID")),
                               reader.GetString(reader.GetOrdinal("CollDate")),
                               reader.GetString(reader.GetOrdinal("NumUnits")),
                               reader.GetString(reader.GetOrdinal("AnimalName")),
                               reader.GetString(reader.GetOrdinal("Breed")),
                               reader.GetString(reader.GetOrdinal("RegNum")),
                               reader.GetString(reader.GetOrdinal("PersonName")),
                               reader.GetString(reader.GetOrdinal("City")),
                               reader.GetString(reader.GetOrdinal("State")),
                               reader.GetString(reader.GetOrdinal("Country")),
                               reader.GetString(reader.GetOrdinal("Species")));
                            resultList.Add(searchResult);
                        }
                        connection.Close();
                        return resultList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database.");
                return new List<SearchResult>();
            }
        }
    }
}
