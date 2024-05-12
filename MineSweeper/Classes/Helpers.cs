namespace MineSweeper.Classes
{
    public static class Helpers
    {
        public enum DifficultyLevel
        {
            Easy,
            Medium,
            Hard
        }

        public struct Coords
        {
            public int X;
            public int Y;

            public Coords(int i, int j) : this()
            {
                X = i;
                Y = j;
            }
        }
    }
}
