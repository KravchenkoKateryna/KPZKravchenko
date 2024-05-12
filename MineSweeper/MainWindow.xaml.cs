using MineSweeper.Classes;
using System.Windows;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new GameField(Helpers.DifficultyLevel.Easy).Show();
            return;
        }
    }
}