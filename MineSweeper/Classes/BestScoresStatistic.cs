using System.IO;
namespace MineSweeper.Classes
{
    internal class BestScoresStatistic
    {
        const string FILE_NAME = "bestScores.txt";

        private List<BestScoreItem> BestScores { get; set; }

        public BestScoresStatistic()
        {
            BestScores = new List<BestScoreItem>();
            LoadBestScores();
        }

        private void LoadBestScores()
        {
            if (!File.Exists(FILE_NAME))
                return;

            var lines = File.ReadAllLines(FILE_NAME);
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                BestScores.Add(new BestScoreItem
                {
                    Name = parts[0],
                    Time = int.Parse(parts[1]),
                    Difficulty = parts[2]
                });
            }
        }
    }
}
