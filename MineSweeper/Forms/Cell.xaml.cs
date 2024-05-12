using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MineSweeper.Forms
{
    public partial class Cell : UserControl
    {
        private int _bombsAround = 0;
        public Action<Cell> LMBClick;
        public Action BombClick;
        public Action CellIsOpened;
        public Action<int> BombMarked;

        private bool _isFlaged = false;

        public int XCoord {  get; set; }
        public int YCoord {  get; set; }
        public bool IsPressed => !cellBtn.IsEnabled;

        public Cell()
        {
            InitializeComponent();
        }

        public bool IsBomb() => _bombsAround == -1;
        
        public void DrawPressedButton()
        {
            if (_isFlaged)
                return;

            Dictionary<int, Color> cellColors = new Dictionary<int, Color>
            {
                { 1, Colors.Aqua },
                { 2, Colors.Green },
                { 3, Colors.Red },
                { 4, Colors.Blue },
                { 5, Colors.Purple },
                { 6, Colors.Orange },
                { 7, Colors.Aquamarine },
                { 8, Colors.Orchid }
            };

            cellBtn.Content = _bombsAround == 0 ? "" : (_bombsAround == -1 ? "💣" : _bombsAround.ToString());

            cellBtn.Background = new SolidColorBrush(Colors.Red);
            cellBtn.Foreground = new SolidColorBrush(cellColors.ContainsKey(_bombsAround) ? cellColors[_bombsAround] : Colors.Black);
            cellBtn.IsEnabled = false;

            cellBtn.UpdateLayout();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isFlaged) return;

            LMBClick(this);

            if (IsBomb())
            {
                BombClick();
                return;
            }
                
            CellIsOpened();
            DrawPressedButton();
        }


        public void SetBombAround(int amount)
        {
            _bombsAround = amount;
            UpdateLayout();
        }

        private void PutFlag(object sender, RoutedEventArgs e)
        {
            if (_isFlaged)
            {
                cellBtn.Content = "";
                _isFlaged = false;
                BombMarked(-1);
            }
            else
            {
                cellBtn.Content = "🚩";
                _isFlaged = true;
                BombMarked(1);
            }
        }
    }
}
