using MineSweeper.Classes;
using MineSweeper.Classes.Levels;
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

        private void EasyLevelBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new GameField(new EasyLevel()).Show();
        }

        private void MediumLevelBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new GameField(new MediumLevel()).Show();
        }

        private void HardLevelBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new GameField(new HardLevel()).Show();
        }
    }
}