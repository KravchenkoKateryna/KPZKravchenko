using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Classes.Levels
{
    public class CustomLevel : ILevel
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int Bombs { get; }

        public CustomLevel(string name, int width, int height, int bombs)
        {
            Name = name;
            Width = width;
            Height = height;
            Bombs = bombs;
        }
    }
}
