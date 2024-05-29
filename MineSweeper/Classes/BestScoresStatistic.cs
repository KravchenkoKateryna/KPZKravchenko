using MineSweeper.Classes.Levels;
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
                if (parts.Length == 6)
                {
                    ILevel level;
                    switch (parts[2])
                    {
                        case "Easy":
                            level = new EasyLevel();
                            break;
                        case "Medium":
                            level = new MediumLevel();
                            break;
                        case "Hard":
                            level = new HardLevel();
                            break;
                        default:
                            level = new CustomLevel(parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                            break;
                    }

                    BestScores.Add(new BestScoreItem
                    {
                        Name = parts[0],
                        Time = int.Parse(parts[1]),
                        Level = level
                    });
                }
            }
        }


        public void SaveBestScore(string name, int time, ILevel level)
        {
            if (CheckBestScore(level, time))
            {
                BestScores.Add(new BestScoreItem
                {
                    Name = name,
                    Time = time,
                    Level = level
                });

                var bestScoresForLevel = BestScores
                    .Where(x => x.Level.Name.Equals(level.Name, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Time)
                    .ToList();

                if (bestScoresForLevel.Count > 10)
                {
                    BestScores = bestScoresForLevel.Take(10).ToList();
                }

                File.WriteAllText(FILE_NAME, string.Join(Environment.NewLine, BestScores.Select(x => $"{x.Name} {x.Time} {x.Level.Name} {x.Level.Width} {x.Level.Height} {x.Level.Bombs}")));
            }
        }

        public bool CheckBestScore(ILevel level, int time)
        {
            var bestScoresForLevel = BestScores
                .Where(x => x.Level.Name.Equals(level.Name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Time)
                .ToList();

            return bestScoresForLevel.Count < 10 || bestScoresForLevel.Any(x => x.Time > time);
        }

        public string GetBestScores(ILevel level)
        {
            var filteredScores = BestScores
                .Where(x => x.Level.Name.Equals(level.Name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Time)
                .ToList();

            var sb = new StringBuilder();
            foreach (var score in filteredScores)
            {
                sb.AppendLine($"{score.Name} {score.Time} {score.Level.Name} {score.Level.Width} {score.Level.Height} {score.Level.Bombs}");
            }

            return sb.ToString();
        }

    }
}
