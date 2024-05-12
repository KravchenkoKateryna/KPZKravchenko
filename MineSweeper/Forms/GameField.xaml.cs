using MineSweeper.Classes.Levels;
using MineSweeper.Forms;
using System.Windows;
using System.Windows.Controls;

namespace MineSweeper
{
    public partial class GameField : Window
    {
        private readonly ILevel _difficultyLevel;
        private int _totalBombs = 0;
        private bool _isBombPlaced = false;
        private Cell[,] cells;
        private int _openedCells = 0;
        private int _bombsMarked = 0;

        public GameField(ILevel difficulty)
        {
            InitializeComponent();

            _difficultyLevel = difficulty;

            GenerateField();
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
                    var cell = new Cell();
                    cells[i, j] = cell;
                    cell.XCoord = i;
                    cell.YCoord = j;
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    bombContainerGrd.Children.Add(cell);
                    cell.LMBClick = CellLMBClick;
                    cell.BombClick = LoseGame;
                    cell.CellIsOpened = () =>
                    {
                        _openedCells++;
                        if (_openedCells == _difficultyLevel.Width * _difficultyLevel.Height - _totalBombs)
                            MessageBox.Show("You won!");
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

        public void CellLMBClick(Cell currentCell)
        {
            if (!_isBombPlaced)
                PlaceBombs(currentCell);

            int x = currentCell.XCoord;
            int y = currentCell.YCoord;

            if (currentCell.BombsAround == 0)
                OpenBombsAround(x, y);
        }

        private void OpenBombsAround(int x, int y)
        {

            if (x < 0 || y < 0 || x >= cells.GetLength(0) || y >= cells.GetLength(1))
                return;

            if (cells[x, y].IsPressed)
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

        private void LoseGame()
        {
            foreach (var cell in cells)
                cell.DrawPressedButton();

            MessageBox.Show("You lost!");
        }

        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            GenerateField();
        }
    }
}
