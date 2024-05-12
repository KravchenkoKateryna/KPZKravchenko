namespace MineSweeper.Classes.Levels
{
    internal class MediumLevel : ILevel
    {
        public string Name { get; } = "Medium";
        public int Width { get; } = 16;
        public int Height { get; } = 16;
        public int Bombs { get; } = 40;
    }
}
