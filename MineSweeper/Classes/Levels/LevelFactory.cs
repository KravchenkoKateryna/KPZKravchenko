using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Classes.Levels
{
    public static class LevelFactory
    {
        public static ILevel GetLevel(string difficulty)
        {
            switch (difficulty)
            {
                case "Easy":
                    return new EasyLevel();
                case "Medium":
                    return new MediumLevel();
                case "Hard":
                    return new HardLevel();
                default:
                    throw new ArgumentException("Invalid difficulty level");
            }
        }
    }
}
