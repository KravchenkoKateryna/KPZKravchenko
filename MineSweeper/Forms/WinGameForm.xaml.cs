using MineSweeper.Classes;
using System.Windows;

namespace MineSweeper.Forms
{
    /// <summary>
    /// Interaction logic for WinGameForm.xaml
    /// </summary>
    public partial class WinGameForm : Window
    {
        private readonly string _difficultyLevel;
        private readonly int _time;
        private readonly GameField _gameField;
        private GameField GameWindow;

        public WinGameForm(string difficultyLevel, int time, GameField gameField)
        {
            InitializeComponent();
            _difficultyLevel = difficultyLevel;
            _time = time;
            _gameField = gameField;

            // Save best score if applicable
            SaveBestScore();
        }

        private void SaveBestScore()
        {
            // Prompt user for their name
            var playerName = PromptForName();
            if (!string.IsNullOrEmpty(playerName))
            {
                BestScoresStatistic.Instance.SaveBestScore(playerName, _time, _difficultyLevel);
            }
        }

        private string PromptForName()
        {
            // Implement a method to prompt the user for their name
            return "Player"; // Placeholder for actual implementation
        }

        private void saveScoreBtn_Click(object sender, RoutedEventArgs e)
        {
            this.SaveBestScore();
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
}
