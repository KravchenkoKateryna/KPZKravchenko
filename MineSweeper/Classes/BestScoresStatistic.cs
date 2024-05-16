using System.IO;
using System.Text;
namespace MineSweeper.Classes;
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

    public string GetBestScores(string Difficulty)
    {
        BestScores = BestScores.OrderBy(x => x.Time).ToList();

        var sb = new StringBuilder();
        foreach (var score in BestScores.Where(x => x.Difficulty == Difficulty))
            sb.AppendLine($"{score.Name} {score.Time} {score.Difficulty}");

        return sb.ToString();
    }

    public bool CheckBestScore(string Difficulty, int Time)
    {
        var bestScore = BestScores.Where(x => x.Difficulty == Difficulty);
        if (bestScore.Count() < 10)
            return true;

        foreach (var score in bestScore)
            if (score.Time > Time)
                return true;

        return false;
    }

    public void SaveBestScore(string Name, int Time, string Difficulty)
    {
        if (CheckBestScore(Difficulty, Time))
        {
            BestScores.Add(new BestScoreItem
            {
                Name = Name,
                Time = Time,
                Difficulty = Difficulty
            });
        }

        var bestScore = BestScores.Where(x => x.Difficulty == Difficulty);
        if (bestScore.Count() > 10)
        {

            BestScores = BestScores.OrderBy(x => x.Time).ToList();
            for (int i = 0; i < BestScores.Count; i++)
                if (BestScores[i].Difficulty == Difficulty)
                    BestScores.RemoveAt(i);
        }

        File.WriteAllText(FILE_NAME, string.Join("\n", BestScores.Select(x => $"{x.Name} {x.Time} {x.Difficulty}")));
    }
}
