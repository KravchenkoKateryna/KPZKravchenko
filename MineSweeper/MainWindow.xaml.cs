using MineSweeper.Classes.Levels;
using System.Windows;

namespace MineSweeper;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void EasyLevelBtn_Click(object sender, RoutedEventArgs e)
    {
        new GameField(new EasyLevel()).Show();
        Close();
    }

    private void MediumLevelBtn_Click(object sender, RoutedEventArgs e)
    {
        new GameField(new MediumLevel()).Show();
        Close();
    }

    private void HardLevelBtn_Click(object sender, RoutedEventArgs e)
    {
        new GameField(new HardLevel()).Show();
        Close();
    }
}