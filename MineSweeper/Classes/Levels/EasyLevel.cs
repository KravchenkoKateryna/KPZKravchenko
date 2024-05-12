namespace MineSweeper.Classes.Levels
{
    internal class EasyLevel : ILevel
    {
        public string Name { get; } = "Easy";
        public int Width { get; } = 10;
        public int Height { get; } = 10;
        public int Bombs { get; } = 10;
    }
}
