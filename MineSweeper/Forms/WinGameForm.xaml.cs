using MineSweeper.Classes;
using MineSweeper.Classes.Levels;
using System.Windows;

namespace MineSweeper.Forms;
public partial class WinGameForm : Window
{
    private  ILevel DifficultyLevel;
    private int Time;
    private GameField GameWindow;
    private BestScoresStatistic bestScoreManager;

    const string _bestScoreText = "You have one of the best scores! Enter your name to save it!";
    const string _noBestScoreText = "You win the game.";

    public WinGameForm(ILevel difficulty, int time, GameField game)
    {
        InitializeComponent();

        GameWindow = game;
        DifficultyLevel = difficulty;
        Time = time;
        BestScoreName.Visibility = Visibility.Collapsed;

        bestScoreManager = new BestScoresStatistic();
        if (bestScoreManager.CheckBestScore(DifficultyLevel, Time))
        {
            BestScoreName.Visibility = Visibility.Visible;
            BestScoreName.Text = string.Empty;

            winText.Content = _bestScoreText;
        }
        else
        {
            winText.Content = _noBestScoreText;
        }
    }

    private void saveScoreBtn_Click(object sender, RoutedEventArgs e)
    {
        bestScoreManager.SaveBestScore(BestScoreName.Text, Time, DifficultyLevel);
        MessageBox.Show(bestScoreManager.GetBestScores(DifficultyLevel));
        GameWindow.GenerateField();
        Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        GameWindow.GenerateField();
        Close();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        GameWindow.Close();
        Close();
    }
}
