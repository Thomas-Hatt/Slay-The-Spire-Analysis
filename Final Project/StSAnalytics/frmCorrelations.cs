using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SpireAnalytics
{
    /// <summary>
    /// In my average run form, I use the following critera to display my information:
    /// 1. The character the player chose (CharacterChosen)
    /// 2. How far the player got into their run (FloorReached)
    /// 3. The player's final score (TotalScore)
    /// 4. Total amount of gold (TotalGold)
    /// 5. Total amount of relics (indexing "Relics")
    /// 6. Game difficulty (AscensionLevel)
    /// 
    /// 
    /// For this section of the program, I'd like to have 6 charts, similar to how the average runs were formatted.
    /// Chart 1: Correlations between Character Choice and Floor Reached
    /// Chart 2: Correlations between Total Gold and Floor Reached
    /// Chart 3: Correlations between Ascension Level and Floor Reached
    /// Chart 4: Correlations between Total Relics and Floor Reached
    /// Chart 5: Correlations between Ascension Level and Total Relics
    /// Chart 6: Correlations between Character Choice and Total Relics
    /// </summary>
    public partial class frmCorrelations : Form
    {
        // Initialize a variable to store what set of charts will be displayed
        int showing = 0;

        public frmCorrelations()
        {
            InitializeComponent();

            // Initialize lblDisplayingChart text
            lblDisplayingChart.Text = $"Display Charting {showing + 1} of 3";
        }
        private void frmCorrelations_Load(object sender, EventArgs e)
        {
            // Create an instance of HelpProvider
            HelpProvider helpProvider = new HelpProvider();

            // Set btnViewNextCharts to invisible :(
            btnViewNextCharts.Visible = false;

            // Set lblDisplayingChart to invisible :(
            lblDisplayingChart.Visible = false;

            // Assign HelpString to my controls
            helpProvider.SetHelpString(btnViewNextCharts, "This button allows you to view the next 2 charts in the system.");
            helpProvider.SetHelpString(btnClose, "This button closes this form.");

            helpProvider.SetHelpString(chartAnalyzeGoldFloorCorrelations, "This chart displays correlations between how much gold the player obtained during the floor set.");

            helpProvider.SetHelpString(chartCharacterFloorCorrelations, "This chart displays correlations between the character chosen and the floor the player reached.");


            // Display Run Count
            GetRunCount();

            // Ensure the third set of charts are at the front
            chartCharacterFloorCorrelations.BringToFront();
            chartAnalyzeGoldFloorCorrelations.BringToFront();

            // Display charts
            AnalyzeCharacterFloorCorrelations();
            AnalyzeGoldFloorCorrelations();
        }

        /// <summary>
        /// Chart 1: Correlations between Character Choice and Floor Reached
        /// Here I am analyzing the correlations between which character the player chose and the floor that they reached on average.
        /// Most higher level players are convinced that The Watcher is the best character by far for winning,
        /// But is that entirely true? That's the point of this chart!
        /// (Chart is not entirely accurate because The Watcher was released a few months before this data was collected, and players were not efficient with her yet)
        /// </summary>
        private void AnalyzeCharacterFloorCorrelations()
        {
            // List of characters
            string[] characters = { "IRONCLAD", "THE_SILENT", "DEFECT", "WATCHER" };

            // Floor categories
            string[] floorCategories = { "Floor 1-10", "Floor 11-20", "Floor 21-30", "Floor 31-40", "Floor 41-51", "Floor 52+" };

            // Clear existing series in the chart
            chartCharacterFloorCorrelations.Series.Clear();
            chartCharacterFloorCorrelations.ChartAreas[0].AxisY.Minimum = 0;

            try
            {
                // Create a new instance of clsData
                clsData FloorData = new clsData();

                // Loop through each character in the array
                foreach (string character in characters)
                {
                    // Create a separate series for each character
                    Series characterSeries = new Series(character);
                    characterSeries.ChartType = SeriesChartType.Line;
                    characterSeries.BorderWidth = 5;
                    chartCharacterFloorCorrelations.Series.Add(characterSeries);

                    // Loop through the floor cateogires
                    for (int i = 0; i < floorCategories.Length; i++)
                    {
                        // Query to count floor reaches per character
                        string query = $"SELECT COUNT(*) FROM tblRuns WHERE CharacterChosen = '{character}' AND Val(FloorReached) BETWEEN {i * 10 + 1} AND {(i + 1) * 10};";

                        // Execute the query
                        int rowCount = FloorData.ExecuteQuery(query);

                        // Cleanup
                        if (rowCount < 0) rowCount = 0;

                        // Add data point to the character's series
                        int pointIndex = characterSeries.Points.AddXY(floorCategories[i], rowCount);
                    }
                }

                // Adjust view
                chartCharacterFloorCorrelations.ChartAreas[0].RecalculateAxesScale();
            }
            catch (Exception ex)
            {
                // Catach any exceptions / errors
                Console.WriteLine($"ERROR: {ex.Message}");
                MessageBox.Show($"Chart Error: {ex.Message}");
            }
        }


        /// <summary>
        /// Chart 2: Correlations between Total Gold and Floor Reached
        /// For this chart, I am analyzing the Total Gold the player has and the Floor Reached.
        /// On average, Gold will increase throughout a player's run.
        /// However, how much does the gold truly increase? It is absolutely not a linear process.
        /// </summary>
        private void AnalyzeGoldFloorCorrelations()
        {
            // Gold categories (series)
            string[] goldCategories = { "99-300 Gold", "301-600 Gold", "601-900 Gold", "901-1200 Gold", "> 1200 Gold" };

            // Floor categories (X-axis)
            string[] floorCategories = { "Floor 1-15", "Floor 16-30", "Floor 31-40", "Floor 41-51", "Floor 52+" };

            // Clear existing series in the chart
            chartAnalyzeGoldFloorCorrelations.Series.Clear();

            // Set Y-axis minimum to remove unwanted whitespace
            chartAnalyzeGoldFloorCorrelations.ChartAreas[0].AxisY.Minimum = 0;

            try
            {
                // Initialize new clsData
                clsData FloorData = new clsData();

                // Calculate the total runs per floor category
                Dictionary<string, int> totalRunsPerFloor = new Dictionary<string, int>();

                // Loop through the floor category dictionary
                for (int i = 0; i < floorCategories.Length; i++)
                {
                    // SQL query for grouping floor numbers
                    string totalQuery = $"SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) BETWEEN {i * 15 + 1} AND {(i + 1) * 15};";

                    // Execute the query and story it in the dictionary
                    totalRunsPerFloor[floorCategories[i]] = FloorData.ExecuteQuery(totalQuery);
                }

                foreach (string goldRange in goldCategories)
                {
                    // Create a new series
                    Series goldSeries = new Series(goldRange);
                    goldSeries.ChartType = SeriesChartType.Line;
                    goldSeries.BorderWidth = 3;
                    chartAnalyzeGoldFloorCorrelations.Series.Add(goldSeries);

                    // Loop through each floor amount
                    for (int floor = 1; floor <= 60; floor++)
                    {
                        // Initialize variables
                        int goldMin, goldMax;


                        // Set gold ranges
                        if (goldRange == "99-300 Gold")
                        {
                            goldMin = 99; goldMax = 300;
                        }

                        else if (goldRange == "301-600 Gold")
                        {
                            goldMin = 301; goldMax = 600;
                        }

                        else if (goldRange == "601-900 Gold")
                        {
                            goldMin = 601; goldMax = 900;
                        }

                        else if (goldRange == "901-1200 Gold")
                        {
                            goldMin = 901; goldMax = 1200;
                        }

                        else
                        {
                            goldMin = 1201; goldMax = int.MaxValue;
                        }

                        // Select all instances between the gold amounts and the floor reached
                        string querySelection = $"SELECT COUNT(*) FROM tblRuns WHERE Gold BETWEEN {goldMin} AND {goldMax} AND Val(FloorReached) = {floor};";

                        // Execute query
                        int occurrences = FloorData.ExecuteQuery(querySelection);

                        // Select all instances where floor reached is equal to the floor number
                        string queryTotal = $"SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) = {floor};";

                        // Execute query
                        int totalFloorRuns = FloorData.ExecuteQuery(queryTotal);

                        // Calculate the probability
                        double probability = (totalFloorRuns > 0) ? (occurrences * 100.0 / totalFloorRuns) : 0;

                        // Add the point
                        int pointIndex = goldSeries.Points.AddXY($"Floor {floor}", probability);
                    }
                }

                // Adjust view
                chartAnalyzeGoldFloorCorrelations.ChartAreas[0].RecalculateAxesScale();
            }
            catch (Exception ex)
            {
                // Catch any exceptions
                Console.WriteLine($"ERROR: {ex.Message}");
                MessageBox.Show($"Chart Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays the run count dynamically in a scaling way
        /// </summary>
        private void GetRunCount()
        {
            clsData VideoCount = new clsData();
            VideoCount.SQL = "SELECT COUNT(ID) AS NumRuns FROM tblRuns";
            tmrSystemRuns.Tag = VideoCount.Dt.Rows[0]["NumRuns"].ToString();
            tmrSystemRuns.Start();
        }

        private void tmrSystemRuns_Tick(object sender, EventArgs e)
        {
            int intTotalCount = Convert.ToInt32(tmrSystemRuns.Tag);
            int intCurrentCount = Convert.ToInt32(lblRunsAnalyzed.Text);
            var rand = new Random();
            int intIncrement = rand.Next(900, 1100);
            if (intCurrentCount + intIncrement >= intTotalCount)
            {
                lblRunsAnalyzed.Text = intTotalCount.ToString();
                tmrSystemRuns.Stop();
            }
            else
            {
                intCurrentCount += intIncrement;
                lblRunsAnalyzed.Text = intCurrentCount.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close form
            this.Close();
        }

        /// <summary>
        /// There are 6 total charts on this page, so I need a way to flip through them.
        /// I initialized the integer "showing" at 0, which I increment and eventually reset when the user clicks the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        /*
        private void btnViewNextCharts_Click(object sender, EventArgs e)
        {
            switch (showing)
            {
                case 0:
                    // First set of charts
                    chartAscensionCorrelation.BringToFront();
                    chartScoreCorrelation.BringToFront();

                    // Increment showing (now '1')
                    showing++;

                    break;
                case 1:
                    // Second set of charts
                    chartCharacterCorrelation.BringToFront();
                    chartRelicsCorrelation.BringToFront();

                    // Increment showing (now '2')
                    showing++;

                    break;
                case 2:
                    // Third set of charts (starts here)
                    chartCharacterFloorCorrelations.BringToFront();
                    chartAnalyzeGoldFloorCorrelations.BringToFront();

                    // Reset showing variable
                    showing = 0;

                    break;
                default:
                    // Default error
                    MessageBox.Show("Error occurred while displaying charts");
                    Console.WriteLine($"Unexpected value in 'showing': {showing}");

                    // Close the form
                    this.Close();

                    break;
            }

            // Update lblDisplayingChart text to reflect the new "showing" update
            lblDisplayingChart.Text = $"Displaying Chart {showing + 1} of 3";
        }

        */
    }
}