using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions; // "(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)"

namespace SpireAnalytics
{
    public partial class frmImport : Form
    {

        static Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
        public frmImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is where the user clicks the Import button to import merged_runs.csv.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmImport_Load(object sender, EventArgs e)
        {
            // Display a message on load
            UpdateStatusList("Waiting for file import.");

            // Create an instance of HelpProvider
            HelpProvider helpProvider = new HelpProvider();

            // Assign HelpString to my controls
            helpProvider.SetHelpString(btnImport, "This button allows you to import the data from the CSV file.");
            helpProvider.SetHelpString(btnClose, "This button closes this form.");
            helpProvider.SetHelpString(lstImport, "This list displays the status of the imports.");
        }


        /// <summary>
        /// Updates the list with the line number we are currently importing
        /// </summary>
        /// <param name="Status"></param>
        private void UpdateStatusList(string Status)
        {
            // Add the item passed
            lstImport.Items.Add(Status);

            // Change the selected index to the most recent item passed
            lstImport.SelectedIndex = lstImport.Items.Count - 1;
        }

        /// <summary>
        /// Closes this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }


        /// <summary>
        /// Imports the CSV file needed for running the other sections of the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            // Interface handling
            btnImport.Enabled = false;
            btnClose.Enabled = false;

            // Get the user to select a CSV file
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            ofdImport.Title = "Browse for CSV Input Files";
            ofdImport.DefaultExt = "csv";
            var result = ofdImport.ShowDialog();

            // Check to see if the file name is empty
            if (result == DialogResult.Cancel)
            {
                // Display error message and return
                MessageBox.Show("A file must be selected to continue! Please try again.");
                this.Close();
                return;
            }

            // Check to see if the user selected a file
            if (ofdImport.CheckFileExists == true)
            {
                // Clean the data
                int intRecordCount = CleanDataSource(ofdImport.FileName);

                // Import from CSV
                ImportRunsFromCSV("merged_runs.csv", intRecordCount);
            }

            // Interface Handling
            btnImport.Enabled = true;
            btnClose.Enabled = true;
        }


        /// <summary>
        /// Imports run data from the merged_runs csv file and updates the database.
        /// It validates the data while ensuring minimum values for Gold and Relics,
        /// and updates the database after processing all valid entries.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="totalCount"></param>
        public void ImportRunsFromCSV(string path = "merged_runs.csv", int totalCount = 0)
        {
            List<clsRunDetail> RunList = new List<clsRunDetail>();
            string line;
            int count = 0;
            StreamReader file = new StreamReader(path);

            // Remove all data from database
            clsData DeleteData = new clsData();
            DeleteData.DeleteRecord("DELETE * FROM tblRuns");

            // Track the header
            bool isHeader = true;

            while ((line = file.ReadLine()) != null)
            {
                if (isHeader)
                {
                    // Skip first row (header)
                    isHeader = false;
                    continue;
                }

                // Increment by 1
                count += 1;

                // Update the status
                UpdateStatusList(count.ToString("N0") + " of " + totalCount.ToString("N0") + " records reviewed.");

                string[] columns = SplitCSV(line);

                if (columns.Length < 49)
                {
                    Console.WriteLine($"Warning: Row {count} has fewer columns than expected!");

                    // Skip corrupt rows
                    continue;
                }

                // 0  : event.gold_per_floor > Gold (indexed and averaged)
                // 1  : event.floor_reached > FloorReached
                // 4  : event.score > Score
                // 20 : event.character_chosen > CharacterChosen
                // 13  : event.relics > "Relics" (indexed)
                // 48  : event.ascension_level > AscensionLevel

                // Process event.gold_per_floor
                string goldDataRaw = columns[0];
                long totalGold = ProcessGoldData(goldDataRaw);

                // Debug lines
                Console.WriteLine($"Debug: columns[4] (score) = '{columns[4]}'");
                Console.WriteLine($"Processed Total Gold: {totalGold}");

                // Trim data
                columns[4] = columns[4].Trim();

                string ascensionStr = columns.Length > 48 ? columns[48] : "0";

                // Remove any remaining quotes
                ascensionStr = ascensionStr.Trim().Trim('"');
                if (!int.TryParse(ascensionStr, out int ascensionLevel))
                {
                    ascensionLevel = 0;
                    Console.WriteLine($"Warning: Invalid ascension level at row {count}: '{ascensionStr}'");
                }

                // Relics
                string relicDataRaw = columns[13];
                long totalRelics = ProcessRelicData(relicDataRaw);

                // Debug lines
                Console.WriteLine($"Processed Total Relics: {totalRelics}");

                // Add data to list <clsRunDetail>
                long verifiedGold = totalGold < 99 ? 99 : totalGold; // Ensure 99 minimum Gold
                long verifiedRelics = totalRelics < 1 ? 1 : totalRelics; // Ensure 1 minimum Relic

                clsRunDetail newRun = new clsRunDetail(
                    columns[20],
                    columns[1],
                    columns[4],
                    verifiedGold.ToString(),
                    verifiedRelics.ToString(),
                    ascensionLevel.ToString()
                );
                RunList.Add(newRun);
            }

            UpdateStatusList("Updating Database");

            // Create an instance of clsData
            clsData NewRunData = new clsData();
            NewRunData.BatchAddRuns(RunList);

            // Close file
            file.Close();

            // Update status
            UpdateStatusList("Import Complete!");
        }

        private static string[] SplitCSV(string input)
        {
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (curr.Length == 0)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }

        private int CleanDataSource(string path, string newPath = "merged_runs.csv")
        {
            int count = 0;
            string line;
            StreamReader file = new StreamReader(path);

            StreamWriter newFile = new StreamWriter(newPath, false);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("\uFEFF", "").Trim();

                if (!line.StartsWith("\\n"))
                {
                    if (!line.EndsWith("\""))
                    {
                        line += "\"";
                    }

                    count += 1;
                    newFile.WriteLine(line);
                }
            }

            // Close the files
            newFile.Close();
            file.Close();

            UpdateStatusList(count.ToString() + " records to be imported.");
            return count;
        }

        /// <summary>
        /// I need a way to process the gold the player has accumulated across the run.
        /// There is "event.gold" in the CSV file, however that only tracks the gold the player has at the end of the run.
        /// Therefore, I need a way to loop through the current data that looks something like this:
        /// [18,36,56,66,66,91,101,135,162,162,10,0,0]
        /// The player normally starts with 99 gold, however there is a starting bonus that removes all gold the player has, so we have to accomodate for that.
        /// Also, I need to make sure that only the gold increases are tracked.
        /// For example, going from 99 -> 0 -> 18 is a total increase of 18.
        /// However, going from 162 -> 10 should not affect the total gold the player has throughout the run.
        /// </summary>
        /// <param name="goldDataRaw"></param>
        /// <returns></returns>

        public long ProcessGoldData(string goldDataRaw)
        {
            // Handle empty/null cases first
            if (string.IsNullOrWhiteSpace(goldDataRaw))
            {
                // Default starting gold
                return 99;
            }

            // Clean the input string
            goldDataRaw = goldDataRaw.Trim();

            // Remove invalid characters (e.g., stray brackets, quotes)
            goldDataRaw = goldDataRaw.Replace("[", "").Replace("]", "").Replace("\"", "");

            // Split the values
            string[] goldValues = goldDataRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // The player starts with 99 gold
            int previousGold = 99;
            long totalGold = 99;

            foreach (string goldString in goldValues)
            {
                string cleanValue = goldString.Trim();
                if (int.TryParse(cleanValue, out int currentGold))
                {
                    // Debug output for each value
                    Console.WriteLine($"Processing gold value: {currentGold} (Previous: {previousGold})");

                    // Only add increases of gold
                    if (currentGold > previousGold)
                    {
                        totalGold += (currentGold - previousGold);
                    }
                    previousGold = currentGold;
                }
                else
                {
                    Console.WriteLine($"Warning: Invalid gold value '{goldString}'");
                }
            }

            // Final validation
            if (totalGold < 99)
            {
                Console.WriteLine($"Warning: Calculated gold {totalGold} is less than starting 99");
                totalGold = 99;
            }

            Console.WriteLine($"Final calculated gold: {totalGold}");
            return totalGold;
        }


        /// <summary>
        /// Relics are powerful artifacts that the player can obtain from events, chests, elites, or bosses.
        /// The player always starts with 1 relic (called the starter relic), and can obtain more throughout the run.
        /// Chests, Elites, and Bosses will always give at least 1 relic, While certain events have a chance to give you a relic.
        /// These relics are stored in a way similar to Gold, such as:
        /// ["Strawberry","DollysMirror"]
        /// So we have to strip that out, and add the total amount of relics that the player has to store it as an integer.
        /// </summary>
        /// <param name="relicDataRaw"></param>
        /// <returns></returns>
        public long ProcessRelicData(string relicDataRaw)
        {
            // If there are any null values, default the relic amount to '1'
            if (string.IsNullOrWhiteSpace(relicDataRaw))
            {
                return 1;
            }

            // Trim and split the relic names from the relic list
            relicDataRaw = relicDataRaw.Trim().Replace("[", "").Replace("]", "").Replace("\"", "");
            string[] relicNames = relicDataRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Total relics = starter (1) + relics obtained
            return relicNames.Length;
        }
    }
}