﻿using MineSweeper.Classes;
using MineSweeper.Classes.Features.Observer;
using MineSweeper.Classes.Levels;
using MineSweeper.Classes.MineSweeper.Classes;
using MineSweeper.Forms;
using System.Windows;
using System.Windows.Controls;

namespace MineSweeper;
public partial class GameField : Window
{
    public partial class GameField : Window, IObserver
    {
        private readonly ILevel _difficultyLevel;
        private Cell[,] cells;
        private int _totalBombs = 0;
        private int _openedCells = 0;
        private int _bombsMarked = 0;
        private bool _isBombPlaced = false;
        private bool _isGameFinished = false;
        private StopWatch _stopWatch;
        private readonly Subject _subject = new Subject();

        public GameField(string difficulty)
        {
            InitializeComponent();
            _difficultyLevel = LevelFactory.GetLevel(difficulty);
            _subject.Attach(this);
            GenerateField();
        }

        public void Update()
        {
            // Update timer if the stopwatch is running
            if (_stopWatch != null)
            {
                SetTimer(_stopWatch.GetFormattedTime());
            }

            // Update mines count
            minesLbl.Content = _totalBombs - _bombsMarked;

            // Refresh the game field layout
            bombContainerGrd.UpdateLayout();
        }

        public void GenerateField()
        {
            bombContainerGrd.Children.Clear();
            bombContainerGrd.RowDefinitions.Clear();
            bombContainerGrd.ColumnDefinitions.Clear();
            _isBombPlaced = false;
            cells = null;

            this.Title = _difficultyLevel.Name;
            _totalBombs = _difficultyLevel.Bombs;
            bombContainerGrd.Width = _difficultyLevel.Width * 30; // bomb size
            bombContainerGrd.Height = _difficultyLevel.Height * 30;
            cells = new Cell[_difficultyLevel.Height, _difficultyLevel.Width];

            for (int i = 0; i < _difficultyLevel.Height; i++)
                bombContainerGrd.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < _difficultyLevel.Width; i++)
                bombContainerGrd.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < _difficultyLevel.Height; i++)
                for (int j = 0; j < _difficultyLevel.Width; j++)
                {
                    var cell = new Cell(_subject);
                    cells[i, j] = cell;
                    cell.Coords = new Helpers.Coords(i, j);
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    bombContainerGrd.Children.Add(cell);
                    cell.LMBClick = CellLMBClick;
                    cell.BombClick = LoseGame;
                    cell.CellIsOpened = () =>
                    {
                        _openedCells++;
                        if (_openedCells == _difficultyLevel.Width * _difficultyLevel.Height - _totalBombs && !_isGameFinished)
                            new WinGameForm(_difficultyLevel.Name, _stopWatch.GetTotalSeconds(), this).Show();
                    };
                    cell.BombMarked = (int bombs) =>
                    {
                        _bombsMarked += bombs;
                        minesLbl.Content = _totalBombs - _bombsMarked;
                    };
                }
            minesLbl.Content = _totalBombs;
            UpdateLayout();
        }

        public void SetTimer(string time)
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                    Dispatcher.Invoke(() => timerLbl.Content = time);
                else
                    timerLbl.Content = time;

            UpdateLayout();
        }
        catch { }
    }

    public void CellLMBClick(Cell currentCell)
    {
        if (!_isBombPlaced)
            PlaceBombs(currentCell);

        int x = currentCell.Coords.X;
        int y = currentCell.Coords.Y;
        if (currentCell.BombsAround == 0)
            OpenBombsAround(x, y);
    }

    private void OpenBombsAround(int x, int y)
    {
        if (x < 0 || y < 0 || x >= cells.GetLength(0) || y >= cells.GetLength(1) || cells[x, y].IsPressed)
            return;

        cells[x, y].DrawPressedButton();

        if (cells[x, y].BombsAround != 0)
            return;

        OpenBombsAround(x - 1, y);
        OpenBombsAround(x + 1, y);
        OpenBombsAround(x, y - 1);
        OpenBombsAround(x, y + 1);
    }

    public void PlaceBombs(Cell currentCell)
    {
        _isBombPlaced = true;
        int placedBombs = 0;
        while (placedBombs < _totalBombs)
            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (i == currentCell.Coords.X && j == currentCell.Coords.Y)
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

        _stopWatch = new StopWatch();
        _stopWatch.ReportTime = SetTimer;
        _stopWatch.Start();
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

    private void LoseGame()
    {
        _stopWatch.Stop();
        _isGameFinished = true;
        foreach (var cell in cells)
            cell.DrawPressedButton();

        MessageBox.Show("You lost!");
    }

    private void restartBtn_Click(object sender, RoutedEventArgs e)
    {
        _openedCells = 0;
        _bombsMarked = 0;
        minesLbl.Content = _totalBombs;
        timerLbl.Content = "00:00";
        _isGameFinished = false;
        GenerateField();
    }

    private void newGameBtn_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void showScoresBtn_Click(object sender, RoutedEventArgs e)
    {
        var best = BestScoresStatistic.Instance.GetBestScores(_difficultyLevel.Name);
        if (string.IsNullOrEmpty(best))
            MessageBox.Show("No best scores yet!");
        else
            MessageBox.Show(best);
    }
    }
}
