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
            new GameField("Easy").Show();
            Close();
        }

        private void MediumLevelBtn_Click(object sender, RoutedEventArgs e)
        {
            new GameField("Medium").Show();
            Close();
        }

        private void HardLevelBtn_Click(object sender, RoutedEventArgs e)
        {
            new GameField("Hard").Show();
            Close();
        }
    }
}