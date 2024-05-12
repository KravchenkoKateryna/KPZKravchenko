using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MineSweeper.Forms
{
    public partial class Cell : UserControl
    {
        private int _bombsAround = 0;
        public Action<Cell> FirstClick;
        public int XCoord {  get; set; }
        public int YCoord {  get; set; }
        public bool IsPressed => !cellBtn.IsEnabled;

        public Cell()
        {
            InitializeComponent();
        }

        public bool IsBomb() => _bombsAround == -1;
        
        private void DrawPressedButton()
        {
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
            FirstClick(this);

            DrawPressedButton();
        }


        public void SetBombAround(int amount)
        {
            _bombsAround = amount;
            UpdateLayout();
        }
    }
}
