namespace MineSweeper.Classes.Levels
{
    internal class EasyLevel : ILevel
    {
        public string Name { get; } = "Easy";
        public int Width { get; } = 9;
        public int Height { get; } = 9;
        public int Bombs { get; } = 10;
    }
}
