using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MineSweeper.Classes;

namespace MineSweeper.Forms;
public partial class Cell : UserControl
{
    public int BombsAround = 0;
    public Action<Cell> LMBClick;
    public Action BombClick;
    public Action CellIsOpened;
    public Action<int> BombMarked;
    public Helpers.Coords Coords { get; set; }

    private bool _isFlaged = false;

    public bool IsPressed => !cellBtn.IsEnabled;

    public Cell()
    {
        InitializeComponent();
    }

    public bool IsBomb() => BombsAround == -1;
    
    public void DrawPressedButton()
    {
        if (_isFlaged || IsPressed)
            return;

        if (BombsAround != -1)
            CellIsOpened();

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

        cellBtn.Content = BombsAround == 0 ? "" : (BombsAround == -1 ? "💣" : BombsAround.ToString());

        cellBtn.Background = new SolidColorBrush(Colors.Red);
        cellBtn.Foreground = new SolidColorBrush(cellColors.ContainsKey(BombsAround) ? cellColors[BombsAround] : Colors.Black);
        cellBtn.IsEnabled = false;

        UpdateLayout();
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
            
        DrawPressedButton();
    }

    public void SetBombAround(int amount)
    {
        BombsAround = amount;
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
