namespace MineSweeper.Classes.Levels;
internal class HardLevel : ILevel
{
    public string Name { get; } = "Hard";
    public int Width { get; } = 30;
    public int Height { get; } = 16;
    public int Bombs { get; } = 99;
}
