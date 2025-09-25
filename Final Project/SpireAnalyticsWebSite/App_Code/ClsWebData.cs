using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace SpireAnalytics
{
    public class ClsWebData
    {

        // Variable to link to the connection stirng property
        string _strConnectionString = ClsWebGlobal.DatabaseConnectionString;

        // Variable to link to the SQL property
        string _strSQL = "";

        // String to contain the string to the database
        public string ConnectionString
        {
            get { return _strConnectionString; }
            set
            {
                _strConnectionString = value;
                FillDataTable();
            }
        }

        // Store the query to the database
        public string SQL
        {
            get { return _strSQL; }
            set
            {
                _strSQL = value;
                FillDataTable();
            }
        }

        // Data table accessible from the application
        public DataTable Dt { get; set; }

        // Fill our data table with the data from the database based on properties from SQL and the Connection String
        private void FillDataTable()
        {
            // If the Connection String and the SQL is filled, continue
            if (ConnectionString != "" && SQL != "")
            {
                // Create a connection to the database
                OleDbConnection conn = new OleDbConnection(ConnectionString);

                // Open the connection to the database
                conn.Open();

                // Create a dataset
                DataSet ds = new DataSet();

                // Fill the dataset with the data adapter
                OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, ConnectionString);
                adapter.Fill(ds);

                // Close the connection to the database
                conn.Close();

                // Fill the data table with the dataset
                Dt = ds.Tables[0];
            }
        }

        public void BatchAddRuns(List<ClsWebRunDetail> RunsList)
        {

            // SQL statement
            SQL = "SELECT CharacterChosen, FloorReached, Score, Gold, Relics, AscensionLevel FROM tblRuns WHERE ID = 0";

            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();

                // Create dataset
                DataSet ds = new DataSet();

                // Fill dataset with data adapter
                OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, ConnectionString);
                adapter.Fill(ds);

                // Loop through list
                foreach (var run in RunsList)
                {
                    // Create data row
                    DataRow dr = ds.Tables[0].NewRow();

                    // Update values in data row
                    dr["CharacterChosen"] = run.CharacterChosen.Length > 255 ? run.CharacterChosen.Substring(0, 255) : run.CharacterChosen;
                    dr["FloorReached"] = run.FloorReached.Length > 255 ? run.FloorReached.Substring(0, 255) : run.FloorReached;
                    dr["Score"] = run.TotalScore;
                    dr["Gold"] = run.GoldPerRun;
                    dr["Relics"] = run.TotalRelics;
                    dr["AscensionLevel"] = run.AscensionLevel;

                    Console.WriteLine($"Debug: Adding row - Character: {run.CharacterChosen}, Gold: {run.GoldPerRun}");

                    // Add data row to table
                    ds.Tables[0].Rows.Add(dr);
                }

                // Create command builder
                OleDbCommandBuilder cb = new OleDbCommandBuilder(adapter);
                Console.WriteLine($"Insert Command: {cb.GetInsertCommand().CommandText}");

                try
                {
                    // Update adapter
                    adapter.Update(ds.Tables[0]);
                    Console.WriteLine("Debug: Database update completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating database: {ex.Message}");
                }
            }
        }

        public void DeleteRecord(string _SQLStatement)
        {
            // Create a connection to the database
            OleDbConnection conn = new OleDbConnection(ConnectionString);

            // Open the connection to the database
            conn.Open();

            // Create a command
            OleDbCommand cmd = new OleDbCommand(_SQLStatement, conn);

            // Execute the command
            cmd.ExecuteNonQuery();

            // Close the connection
            conn.Close();
        }

        public void UpdateData(DataTable _DataTable, string _SQLStatement)
        {
            // Update property with SQL Statement
            SQL = _SQLStatement;

            // Check to see if the Connection String and the SQL is filled
            if (ConnectionString != "" && SQL != "")
            {
                // Create a connection to the database
                OleDbConnection conn = new OleDbConnection(ConnectionString);

                // Open the connection to the database
                conn.Open();

                // Create a dataset
                DataSet ds = new DataSet();

                // Fill the dataset with the data adapter
                OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, ConnectionString);
                adapter.Fill(ds);

                // Create command builder
                System.Data.OleDb.OleDbCommandBuilder cb = new System.Data.OleDb.OleDbCommandBuilder(adapter);

                // Update the database with the data table
                adapter.Update(_DataTable);

                // Close the connection to the database
                conn.Close();
            }
        }

        public int ExecuteScalarQuery(string query)
        {
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);

                // Returns first column of first row
                object result = cmd.ExecuteScalar();

                // Handle any null values
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }
    }
}
