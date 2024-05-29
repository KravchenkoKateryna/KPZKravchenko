using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Classes.Levels
{
    public static class LevelFactory
    {
        public static ILevel GetLevel(string difficulty, int width = 0, int height = 0, int bombs = 0)
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
                    if (width > 0 && height > 0 && bombs > 0)
                    {
                        // Additional checks
                        if (width < 5 || width > 30)
                        {
                            throw new ArgumentException("Width must be between 5 and 30");
                        }
                        if (height < 5 || height > 30)
                        {
                            throw new ArgumentException("Height must be between 5 and 30");
                        }
                        if (bombs < 1 || bombs > width * height / 2)
                        {
                            throw new ArgumentException("Number of bombs must be between 1 and half of the cells");
                        }
                        return new CustomLevel(difficulty, width, height, bombs);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid difficulty level");
                    }
            }
        }
    }
}
