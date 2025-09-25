using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SpireAnalytics
{
    /// <summary>
    /// To find the average run, we need to analyze the following properties of the run:
    /// 1. The character the player chose (CharacterChosen)
    /// 2. How far the player got into their run (FloorReached)
    /// 3. The player's final score (TotalScore)
    /// 4. Total amount of gold (TotalGold)
    /// 5. Total amount of relics (indexing "Relics")
    /// 6. Game difficulty (AscensionLevel)
    /// We'll need to filter out edge cases that would affect data integrity.
    /// For example, if the player has any circlets, then they are cheating.
    /// </summary>
    public partial class frmAvgRun : Form
    {
        public frmAvgRun()
        {
            InitializeComponent();
        }

        // Initialize a variable to store what set of charts will be displayed
        private bool showingFirstSet = true;
        private void frmAvgRun_Load(object sender, EventArgs e)
        {
            // Create an instance of HelpProvider
            HelpProvider helpProvider = new HelpProvider();

            // Assign HelpString to my controls
            helpProvider.SetHelpString(btnViewNextCharts, "This button allows you to view the next 3 charts in the system.");
            helpProvider.SetHelpString(btnClose, "This button closes this form.");

            helpProvider.SetHelpString(chartAscension, "This chart displays the distribution of ascension levels during runs.");
            helpProvider.SetHelpString(chartCharacter, "This chart displays the distribution of the character chosen during runs.");
            helpProvider.SetHelpString(chartFloorReached, "This chart displays the distribution of the final floor reached during runs.");
            helpProvider.SetHelpString(chartGoldDist, "This chart displays the distribution of total gold amount during runs.");
            helpProvider.SetHelpString(chartRelics, "This chart displays the distribution of total relic amount during runs.");
            helpProvider.SetHelpString(chartScore, "This chart displays the distribution of total score amount during runs.");

            // Display Run Count
            GetRunCount();

            // Display charts
            GetGoldChart();
            GetFloorReachedChart();
            GetScoreChart();
            GetCharacterChart();
            GetAscensionChart();
            GetRelicChart();
        }

        /// <summary>
        /// For AnalyzeCharacterFloorCorrelations(), I query the database and evaluate the bar sections from the following criteria:
        /// Low Gold: 99–300 Gold
        /// Moderate Gold: 301–600 Gold
        /// High Gold: 601–900 Gold
        /// Very High Gold: 901–1,200 Gold
        /// Highest Gold: 1,200+ Gold
        /// It's important to note that the bare minimum amount of gold is 99 since the player starts their run with 99 gold.
        /// In frmImport, I already added this detail when adding values to the database, so it isn't necessary to be handled here.
        /// </summary>
        private void GetGoldChart()
        {
            // Initialize chart if needed
            if (chartGoldDist.Series["Data"] == null)
            {
                chartGoldDist.Series.Add("Data");
            }

            // Clear the previous data
            chartGoldDist.Series["Data"].Points.Clear();

            try
            {
                // create a new instance of clsData
                clsData GoldData = new clsData();

                try
                {
                    int count = GoldData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns");
                    Console.WriteLine($"Total records: {count}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }

                // Categorizations of total gold the player has (x-axis)
                string[] categories = { "99-300 Gold", "301-600 Gold", "601-900 Gold", "901-1200 Gold", "> 1200 Gold" };

                // List for SQL queries
                string[] queries =
                {
                    "SELECT COUNT(*) FROM tblRuns WHERE Gold BETWEEN 99 AND 300",
                    "SELECT COUNT(*) FROM tblRuns WHERE Gold BETWEEN 301 AND 600",
                    "SELECT COUNT(*) FROM tblRuns WHERE Gold BETWEEN 601 AND 900",
                    "SELECT COUNT(*) FROM tblRuns WHERE Gold BETWEEN 901 AND 1200",
                    "SELECT COUNT(*) FROM tblRuns WHERE Gold > 1200"
                };

                // Loop through each category
                for (int i = 0; i < categories.Length; i++)
                {
                    int rowCount = GoldData.ExecuteQuery(queries[i]);

                    // Debug line
                    Console.WriteLine($"DEBUG: {categories[i]} = {rowCount}");

                    // Ensure non-negative row count
                    if (rowCount < 0) rowCount = 0;

                    // Add the data to the chart (categories in the x-axis, amount of rows in the y-axis)
                    chartGoldDist.Series["Data"].Points.AddXY(categories[i], rowCount);

                }

                // Adjust view
                chartGoldDist.ChartAreas[0].RecalculateAxesScale();

                // Debug line
                Console.WriteLine($"Total records in database: {GoldData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                MessageBox.Show($"Chart Error: {ex.Message}");
            }
        }


        /// <summary>
        /// I created my Floor Reached chart as a pyramid, since I believe it offers the best visual representation.
        /// The floor number signifies how far the player has gotten into their run, and is signified by the following data:
        /// Act 1: Floors 1-17 (16 is a boss fight, 17 is the boss chest)
        /// Act 2: Floors 18-34 (33 is a boss fight, 34 is the boss chest)
        /// Act 3: Floors 35-51 (50 is always a boss battle, 51 is an unknown entity (the heart))
        ///   - If the player has the Ruby, Emerald, and Sapphire keys they will advance to floor 52 and Act 4.
        ///   - Otherwise, the game will end in a "Victory?" screen.
        ///   
        /// Act 4 is special since it is a secret ending, therefore it's important to categorize it here:
        /// Act 4: Floor 52 (floor 53 on Ascension 20) is always a rest site.
        /// Floor 53 (floor 54 on Ascension 20) is always a merchant.
        /// Floor 54 (floor 55 on Ascension 20) is always an Elite encounter against the Spire Shield and Spire Spear.
        /// Floor 55 (floor 56 on Ascension 20) is always a boss battle against the Corrupt Heart.
        /// 
        /// in Runs.accdb, FloorReached is stored as a short text, so it needs to be converted to an integer here before I use it in my query.
        /// </summary>
        private void GetFloorReachedChart()
        {
            // Initialize chart if needed
            if (chartFloorReached.Series["Data"] == null)
            {
                chartFloorReached.Series.Add("Data");
            }

            // Clear the previous data
            chartFloorReached.Series["Data"].Points.Clear();
            chartFloorReached.Annotations.Clear();

            try
            {
                // Create a new instance of clsData
                clsData FloorData = new clsData();

                // Get the total number of runs (for percentage calculation)
                int totalRuns = FloorData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns");
                Console.WriteLine($"Total records: {totalRuns}");

                // Categories for floors (x-axis)
                string[] categories = { "Floor 1-10", "Floor 11-20", "Floor 21-30", "Floor 31-40", "Floor 41-51", "Floor 52+" };

                // SQL queries (converted from short-text to int)
                string[] queries =
                {
                    "SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) BETWEEN 1 AND 10;",
                    "SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) BETWEEN 11 AND 20;",
                    "SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) BETWEEN 21 AND 30;",
                    "SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) BETWEEN 31 AND 40;",
                    "SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) BETWEEN 41 AND 51;",
                    "SELECT COUNT(*) FROM tblRuns WHERE Val(FloorReached) >= 52;"
                };

                // Loop through each category
                for (int i = 0; i < categories.Length; i++)
                {
                    int rowCount = FloorData.ExecuteQuery(queries[i]);

                    // Debug line
                    Console.WriteLine($"DEBUG: {categories[i]} = {rowCount}");

                    // Ensure non-negative row count
                    if (rowCount < 0) rowCount = 0;

                    // Calculate percentage
                    double percentage = totalRuns > 0 ? ((double)rowCount / totalRuns) * 100 : 0;

                    // Add the data to the chart
                    int pointIndex = chartFloorReached.Series["Data"].Points.AddXY(categories[i], rowCount);
                    DataPoint point = chartFloorReached.Series["Data"].Points[pointIndex];

                    // Set legend text for this data point
                    point.LegendText = categories[i];
                    chartFloorReached.Legends[0].Docking = Docking.Left;

                    // Display percentage next to data points
                    point.Label = $"{percentage:F1}%";

                    // Add annotation to display percentage on the pyramid
                    TextAnnotation annotation = new TextAnnotation
                    {
                        Text = $"{percentage:F1}%",
                        X = i - 0.5, // Adjust position
                        Y = rowCount,
                        Font = new Font("Arial", 10),
                        ForeColor = Color.Black,
                        AnchorDataPoint = point
                    };
                }

                // Adjust view
                chartFloorReached.ChartAreas[0].RecalculateAxesScale();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                MessageBox.Show($"Chart Error: {ex.Message}");
            }
        }

        /// <summary>
        /// For me, score is a very illusive metric to base something upon. Even upon winning the game, your score can vary hundreds from another player.
        /// The wiki provides a detailed view on how score is calculated. For example, for every floor that the player climbs, they gain 5 score.
        /// Defeating all 5 bosses grants you 750 score, etc.
        /// 
        /// Here's the criteria that I will base my chart upon:
        /// 1. 50–300 Score  
        /// 2. 300–1000 Score
        /// 3. 1000–1500 Score
        /// 4. 1500–1900 Score
        /// 5. 1901+ Score

        /// </summary>
        private void GetScoreChart()
        {
            // Initialize chart if needed
            if (chartScore.Series["Data"] == null)
            {
                chartScore.Series.Add("Data");
            }

            // Clear the previous data
            chartScore.Series["Data"].Points.Clear();

            try
            {
                // create a new instance of clsData
                clsData ScoreData = new clsData();

                try
                {
                    int count = ScoreData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns");
                    Console.WriteLine($"Total records: {count}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }

                // Categorizations of total score the player obtained (x-axis)
                string[] categories = { "50–150 Score", "151-250 Score", "251–500 Score", "501-900 Score", "901–1500 Score", "1501–1900 Score", "> 1900 Score" };

                // List for SQL queries
                string[] queries =
                {
                    "SELECT COUNT(*) FROM tblRuns WHERE Score BETWEEN 50 AND 150",
                    "SELECT COUNT(*) FROM tblRuns WHERE Score BETWEEN 151 AND 250",
                    "SELECT COUNT(*) FROM tblRuns WHERE Score BETWEEN 251 AND 500",
                    "SELECT COUNT(*) FROM tblRuns WHERE Score BETWEEN 501 AND 900",
                    "SELECT COUNT(*) FROM tblRuns WHERE Score BETWEEN 901 AND 1500",
                    "SELECT COUNT(*) FROM tblRuns WHERE Score BETWEEN 1501 AND 1900",
                    "SELECT COUNT(*) FROM tblRuns WHERE Score > 1900"
                };

                // Loop through each category
                for (int i = 0; i < categories.Length; i++)
                {
                    int rowCount = ScoreData.ExecuteQuery(queries[i]);

                    // Debug line
                    Console.WriteLine($"DEBUG: {categories[i]} = {rowCount}");

                    // Ensure non-negative row count
                    if (rowCount < 0) rowCount = 0;

                    // Add the data to the chart (categories in the x-axis, amount of rows in the y-axis)
                    chartScore.Series["Data"].Points.AddXY(categories[i], rowCount);
                }

                // Adjust view
                chartScore.ChartAreas[0].RecalculateAxesScale();

                // Debug line
                Console.WriteLine($"Total records in database: {ScoreData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                MessageBox.Show($"Chart Error: {ex.Message}");
            }
        }


        /// <summary>
        /// There are 4 characters that the player can choose from in Slay the Spire:
        /// 1. The Ironclad (stored as IRONCLAD)
        /// 2. The Silent (stored as THE_SILENT)
        /// 3. The Defect (stored as DEFECT)
        /// 4. The Watcher (stored as WATCHER)
        /// Out of all 4 characters, The Watcher was released a few months before the data was collected. 
        /// This fact might skew the statistics, but it's still important to analyze nontheless.
        /// It's also important to note that The Ironclad is unlocked by default, and you unlock the other characters as you play the game.
        /// </summary>
        private void GetCharacterChart()
        {
            // Initialize chart if needed
            if (chartCharacter.Series["Data"] == null)
            {
                chartCharacter.Series.Add("Data");
            }

            // Clear previous data
            chartCharacter.Series["Data"].Points.Clear();
            chartCharacter.Annotations.Clear();

            try
            {
                clsData CharacterData = new clsData();

                // Get total count
                string totalCountQuery = "SELECT COUNT(*) FROM tblRuns;";
                int totalCharacterCount = CharacterData.ExecuteQuery(totalCountQuery);

                // Queries (match DB case: ALL CAPS)
                string[] queries =
                {
                    "SELECT COUNT(*) FROM tblRuns WHERE CharacterChosen = 'IRONCLAD';",
                    "SELECT COUNT(*) FROM tblRuns WHERE CharacterChosen = 'THE_SILENT';",
                    "SELECT COUNT(*) FROM tblRuns WHERE CharacterChosen = 'DEFECT';",
                    "SELECT COUNT(*) FROM tblRuns WHERE CharacterChosen = 'WATCHER';"
                };

                // Display names (title case for legend)
                string[] displayNames = { "Ironclad", "The Silent", "Defect", "Watcher" };

                // Add each character's data to the chart
                for (int i = 0; i < queries.Length; i++)
                {
                    int rowCount = CharacterData.ExecuteQuery(queries[i]);
                    double percentage = (rowCount / (double)totalCharacterCount) * 100;

                    // Add point and set legend text
                    int pointIndex = chartCharacter.Series["Data"].Points.AddXY(displayNames[i], rowCount);
                    DataPoint point = chartCharacter.Series["Data"].Points[pointIndex];
                    point.LegendText = $"{displayNames[i]} ({percentage:F2}%)";
                }

                // Refresh chart
                chartCharacter.ChartAreas[0].RecalculateAxesScale();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                MessageBox.Show($"Chart Error: {ex.Message}");
            }
        }


        /// <summary>
        /// There are different difficulties in the game, which is termed "Ascensions".
        /// The default Ascension level is 0, and to unlock more ascension levels, you have to win the previous ascension level.
        /// Therefore, if you'd like to unlock Ascension 20, you have to beat Ascension 0, 1, 2, 3, etc.
        /// It's important to discern the Ascension level, as most players will play on the Default,
        /// But veterans will play the game at the highest difficulty (Ascension 20).
        /// </summary>
        private void GetAscensionChart()
        {
            {
                // Initialize chart if needed
                if (chartAscension.Series["Data"] == null)
                {
                    chartAscension.Series.Add("Data");
                }

                // Clear the previous data
                chartAscension.Series["Data"].Points.Clear();

                try
                {
                    // create a new instance of clsData
                    clsData GoldData = new clsData();

                    try
                    {
                        int count = GoldData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns");
                        Console.WriteLine($"Total records: {count}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                    }

                    // Categorizations of total gold the player has (x-axis)
                    string[] categories = { "Ascension 0", "Ascension 1-5", "Ascension 6-10", "Ascension 11-14", "Ascension 15-19", "Ascension 20" };

                    // List for SQL queries
                    string[] queries =
                    {
                        "SELECT COUNT(*) FROM tblRuns WHERE AscensionLevel = 0",
                        "SELECT COUNT(*) FROM tblRuns WHERE AscensionLevel BETWEEN 1 AND 5",
                        "SELECT COUNT(*) FROM tblRuns WHERE AscensionLevel BETWEEN 6 AND 10",
                        "SELECT COUNT(*) FROM tblRuns WHERE AscensionLevel BETWEEN 11 AND 14",
                        "SELECT COUNT(*) FROM tblRuns WHERE AscensionLevel BETWEEN 15 AND 19",
                        "SELECT COUNT(*) FROM tblRuns WHERE AscensionLevel = 20"
                    };

                    // Loop through each category
                    for (int i = 0; i < categories.Length; i++)
                    {
                        int rowCount = GoldData.ExecuteQuery(queries[i]);

                        // Debug line
                        Console.WriteLine($"DEBUG: {categories[i]} = {rowCount}");

                        // Ensure non-negative row count
                        if (rowCount < 0) rowCount = 0;

                        // Add the data to the chart (categories in the x-axis, amount of rows in the y-axis)
                        chartAscension.Series["Data"].Points.AddXY(categories[i], rowCount);
                    }

                    // Adjust view
                    chartAscension.ChartAreas[0].RecalculateAxesScale();

                    // Debug line
                    Console.WriteLine($"Total records in database: {GoldData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns")}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                    MessageBox.Show($"Chart Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Relics are powerful artifacts that the player can obtain from events, chests, elites, or bosses.
        /// The player always starts with 1 relic (called the starter relic), and can obtain more throughout the run.
        /// Chests, Elites, and Bosses will always give at least 1 relic, While certain events have a chance to give you a relic.
        /// </summary>
        /// <param name="relicDataRaw"></param>
        /// <returns></returns>
        private void GetRelicChart()
        {
            {
                // Initialize chart if needed
                if (chartRelics.Series["Data"] == null)
                {
                    chartRelics.Series.Add("Data");
                }

                // Clear the previous data
                chartRelics.Series["Data"].Points.Clear();

                try
                {
                    // create a new instance of clsData
                    clsData RelicData = new clsData();

                    try
                    {
                        int count = RelicData.ExecuteQuery("SELECT COUNT(*) FROM tblRuns");
                        Console.WriteLine($"Total records: {count}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                    }

                    // Categorizations of total gold the player has (x-axis)
                    string[] categories = { "Relic Count: 1", "Relic Count: 2-5", "Relic Count: 6-10", "Relic Count: 11-14", "Relic Count: 15+" };

                    // List for SQL queries
                    string[] queries =
                    {
                        "SELECT COUNT(*) FROM tblRuns WHERE Relics = 1",
                        "SELECT COUNT(*) FROM tblRuns WHERE Relics BETWEEN 2 AND 5",
                        "SELECT COUNT(*) FROM tblRuns WHERE Relics BETWEEN 6 AND 10",
                        "SELECT COUNT(*) FROM tblRuns WHERE Relics BETWEEN 11 AND 14",
                        "SELECT COUNT(*) FROM tblRuns WHERE Relics >= 15"
                    };

                    // Loop through each category
                    for (int i = 0; i < categories.Length; i++)
                    {
                        int rowCount = RelicData.ExecuteQuery(queries[i]);

                        // Debug line
                        Console.WriteLine($"DEBUG: {categories[i]} = {rowCount}");

                        // Ensure non-negative row count
                        if (rowCount < 0) rowCount = 0;

                        // Add the data to the chart (categories in the x-axis, amount of rows in the y-axis)
                        chartRelics.Series["Data"].Points.AddXY(categories[i], rowCount);
                    }

                    // Adjust view
                    chartRelics.ChartAreas[0].RecalculateAxesScale();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                    MessageBox.Show($"Chart Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Selects all runs from the database and starts the timer
        /// </summary>
        private void GetRunCount()
        {
            clsData RunCount = new clsData();
            RunCount.SQL = "SELECT COUNT(ID) AS NumRuns FROM tblRuns";
            tmrSystemRuns.Tag = RunCount.Dt.Rows[0]["NumRuns"].ToString();
            tmrSystemRuns.Start();
        }


        /// <summary>
        /// A timer system that incrementally updates the runs analyzed label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// I have a simple system that automates switching between the pairs of 3 upon clicking the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewNextCharts_Click(object sender, EventArgs e)
        {
            // First set of charts
            chartGoldDist.Visible = showingFirstSet;
            chartScore.Visible = showingFirstSet;
            chartFloorReached.Visible = showingFirstSet;

            // Second set of charts
            chartCharacter.Visible = !showingFirstSet;
            chartRelics.Visible = !showingFirstSet;
            chartAscension.Visible = !showingFirstSet;

            // Toggle the state
            showingFirstSet = !showingFirstSet;
        }
    }
}