﻿using MineSweeper.Classes.Levels;
using MineSweeper.Forms;
using System.Windows;
using System.Windows.Controls;
using static MineSweeper.Classes.Helpers;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for GameField.xaml
    /// </summary>
    public partial class GameField : Window
    {
        private readonly DifficultyLevel _difficultyLevel;
        private int _totalBombs = 0;
        private bool _isBombPlaced = false;
        private Cell[,] cells;

        public GameField(DifficultyLevel difficulty)
        {
            InitializeComponent();

            _difficultyLevel = difficulty;

            GenerateField(new EasyLevel());
        }

        public void GenerateField(ILevel level)
        {
            this.Title = level.Name;
            _totalBombs = level.Bombs;
            bombContainerGrd.Width = level.Width * 30; // bomb size
            bombContainerGrd.Height = level.Height * 30;
            cells = new Cell[level.Width, level.Height];

            for (int i = 0; i < level.Width; i++)
                bombContainerGrd.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < level.Height; i++)
                bombContainerGrd.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < level.Width; i++)
                for (int j = 0; j < level.Height; j++)
                {
                    var cell = new Cell();
                    cells[i, j] = cell;
                    cell.XCoord = i;
                    cell.YCoord = j;
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    bombContainerGrd.Children.Add(cell);
                    cell.FirstClick = PlaceBombs;
                }

            UpdateLayout();
        }

        public void PlaceBombs(Cell currentCell)
        {
            if (_isBombPlaced)
                return;

            _isBombPlaced = true;

            int placedBombs = 0;
            while (placedBombs < _totalBombs)
                for (int i = 0; i < cells.GetLength(0); i++)
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        if (i == currentCell.XCoord && j == currentCell.YCoord)
                            continue;

                        if (placedBombs < _totalBombs && new Random().Next(cells.Length / _totalBombs) == 1)
                        {
                            cells[i, j].SetBombAround(-1);
                            placedBombs++;
                        }
                    }

            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                    if (!cells[i, j].IsBomb())
                        cells[i, j].SetBombAround(GetBombsAround(i, j));

            UpdateLayout();
        }

        private int GetBombsAround(int x, int y)
        {
            int bombs = 0;
            for (int i = x - 1; i <= x + 1; i++)
                for (int j = y - 1; j <= y + 1; j++)
                    try
                    {
                        bombs += cells[i, j].IsBomb() ? 1 : 0;
                    }
                    catch { }

            return bombs;
        }


    }
}