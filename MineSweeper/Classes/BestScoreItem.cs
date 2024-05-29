using MineSweeper.Classes.Levels;

namespace MineSweeper.Classes;
internal class BestScoreItem
{
    public required string Name { get; set; }
    public int Time { get; set; }
    public ILevel Level { get; set; }
}
