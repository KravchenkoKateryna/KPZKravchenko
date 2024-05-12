namespace MineSweeper.Classes.Levels
{
    public interface ILevel
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int Bombs { get; }
    }
}
