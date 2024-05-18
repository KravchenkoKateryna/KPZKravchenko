namespace MineSweeper.Classes
{
    namespace MineSweeper.Classes
    {
        public class StopWatch
        {
            private DateTime _startTime;
            private TimeSpan _elapsed;
            public Action<string> ReportTime;

            public void Start()
            {
                _startTime = DateTime.Now;
            }

            public void Stop()
            {
                _elapsed = DateTime.Now - _startTime;
            }

            public int GetTotalSeconds()
            {
                return (int)_elapsed.TotalSeconds;
            }

            public string GetFormattedTime()
            {
                var elapsed = DateTime.Now - _startTime;
                return elapsed.ToString(@"mm\:ss");
            }
        }
    }

}
