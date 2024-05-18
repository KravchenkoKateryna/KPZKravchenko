namespace MineSweeper.Classes.Levels
{
    public class EasyLevel : ILevel
    {
        public string Name => "Easy";
        public int Width => 9;
        public int Height => 9;
        public int Bombs => 10;
    }
}
