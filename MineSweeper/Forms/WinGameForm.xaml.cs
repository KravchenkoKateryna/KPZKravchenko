using MineSweeper.Classes;
using System.Windows;

namespace MineSweeper.Forms
{
    /// <summary>
    /// Interaction logic for WinGameForm.xaml
    /// </summary>
    public partial class WinGameForm : Window
    {
        private string Difficulty;
        private int Time;
        private GameField GameWindow;
        private BestScoresStatistic bestScoreManager;

        const string _bestScoreText = "You have one of the best scores! Enter your name to save it!";
        const string _noBestScoreText = "You win the game.";

        public WinGameForm(string difficulty, int time, GameField game)
        {
            InitializeComponent();

            GameWindow = game;
            Difficulty = difficulty;
            Time = time;
            BestScoreName.Visibility = Visibility.Collapsed;

            bestScoreManager = new BestScoresStatistic();
            if (bestScoreManager.CheckBestScore(Difficulty, Time))
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
            bestScoreManager.SaveBestScore(BestScoreName.Text, Time, Difficulty);
            MessageBox.Show(bestScoreManager.GetBestScores(Difficulty));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.GenerateField();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            GameWindow.Close();
            this.Close();
        }
    }
}
