using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SpireAnalytics
{
    public class ClsWebRunDetail
    {
        public ClsWebRunDetail(string _CharacterChosen, string _FloorReached, string _TotalScore, string _GoldPerRun, string _TotalRelics, string _AscensionLevel)
        {
            // Character Chosen (Ironclad, Silent, Defect, or Watcher)
            CharacterChosen = _CharacterChosen;

            // Floor Reached
            FloorReached = _FloorReached;

            // Relics (cleaned)
            if (!long.TryParse(_TotalRelics, out long totalRelics))
            {
                totalRelics = 0; // Default to 0 if parsing fails
            }
            TotalRelics = totalRelics;

            // Score
            if (!long.TryParse(_TotalScore, out long totalScore))
            {
                totalScore = 0; // Default to 0 if parsing fails
            }
            TotalScore = totalScore;

            // Gold per Run
            if (!long.TryParse(_GoldPerRun, out long goldPerRun))
            {
                goldPerRun = 0; // Default to 0 if parsing fails
            }
            GoldPerRun = goldPerRun;

            // Ascension Level
            if (!long.TryParse(_AscensionLevel, out long ascensionLevel))
            {
                ascensionLevel = 0; // Default to 0 if parsing fails
            }
            AscensionLevel = ascensionLevel;

            // Debug output for all fields
            Console.WriteLine($"Debug: CharacterChosen={CharacterChosen}, FloorReached={FloorReached}, TotalScore={TotalScore}, GoldPerRun={GoldPerRun}, TotalRelics={TotalRelics}, AscensionLevel={AscensionLevel}");
        }

        public string CharacterChosen { get; }

        public string FloorReached { get; }

        public long TotalRelics { get; set; }

        public long TotalScore { get; }

        // Gold from each run
        public long GoldPerRun { get; }

        // Total accumulated gold
        public long TotalGold { get; }

        public long AscensionLevel { get; }
    }
}
