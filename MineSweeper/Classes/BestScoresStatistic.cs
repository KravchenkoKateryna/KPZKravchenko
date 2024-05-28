using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MineSweeper.Classes
{
    internal class BestScoresStatistic
    {
        const string FILE_NAME = "bestScores.txt";
        private static BestScoresStatistic _instance;
        private List<BestScoreItem> BestScores { get; set; }

        // Private constructor to prevent instantiation
        public BestScoresStatistic()
        {
            BestScores = new List<BestScoreItem>();
            LoadBestScores();
        }

        public static BestScoresStatistic Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BestScoresStatistic();
                }
                return _instance;
            }
        }

        private void LoadBestScores()
        {
            if (!File.Exists(FILE_NAME))
                return;

            var lines = File.ReadAllLines(FILE_NAME);
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 3)
                {
                    BestScores.Add(new BestScoreItem
                    {
                        Name = parts[0],
                        Time = int.Parse(parts[1]),
                        Difficulty = parts[2]
                    });
                }
            }
        }

        public string GetBestScores(string difficulty)
        {
            var filteredScores = BestScores
                .Where(x => x.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Time)
                .ToList();

            var sb = new StringBuilder();
            foreach (var score in filteredScores)
            {
                sb.AppendLine($"{score.Name} {score.Time} {score.Difficulty}");
            }

            return sb.ToString();
        }

        public bool CheckBestScore(string difficulty, int time)
        {
            var bestScoresForDifficulty = BestScores
                .Where(x => x.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Time)
                .ToList();

            return bestScoresForDifficulty.Count < 10 || bestScoresForDifficulty.Any(x => x.Time > time);
        }

        public void SaveBestScore(string name, int time, string difficulty)
        {
            if (CheckBestScore(difficulty, time))
            {
                BestScores.Add(new BestScoreItem
                {
                    Name = name,
                    Time = time,
                    Difficulty = difficulty
                });

                var bestScoresForDifficulty = BestScores
                    .Where(x => x.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Time)
                    .ToList();

                if (bestScoresForDifficulty.Count > 10)
                {
                    BestScores = bestScoresForDifficulty.Take(10).ToList();
                }

                File.WriteAllText(FILE_NAME, string.Join(Environment.NewLine, BestScores.Select(x => $"{x.Name} {x.Time} {x.Difficulty}")));
            }
        }
    }
}
