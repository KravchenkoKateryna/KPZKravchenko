using MineSweeper.Classes.Levels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MineSweeper;
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
        new GameField("Easy").Show();
        Close();
    }

    private void HardLevelBtn_Click(object sender, RoutedEventArgs e)
    {
        new GameField("Hard").Show();
        Close();
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void CustomLevelBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int width = int.Parse(FieldWidthTbx.Text);
            int height = int.Parse(FieldHeightTbx.Text);
            int mines = int.Parse(FieldMinesTbx.Text);

            new GameField("Custom", width, height, mines).Show();
            Close();
        }
        catch (FormatException)
        {
            MessageBox.Show("Please enter valid numbers for width, height, and mines.");
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}