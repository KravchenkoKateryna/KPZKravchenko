namespace MineSweeper.Classes;
internal class StopWatch
{
    private int _seconds;
    private int _minutes;
    private bool _isRunning;
    private DateTime _startTime;
    private DateTime _endTime;

    public Action<string> ReportTime;

    public void Start()
    {
        _seconds = 0;
        _minutes = 0;
        
        _startTime = DateTime.Now;
        _isRunning = true;
        new TaskFactory().StartNew(CountTime);
    }

    private async Task CountTime()
    {
        while (_isRunning)
        {
            await Task.Delay(1000);
            
            var timeNow = DateTime.Now;
            var time = timeNow - _startTime;
            _seconds = time.Seconds;
            _minutes = time.Minutes;

            ReportTime?.Invoke(GetValue());
        }
    }

    public string GetValue()
    {
        return $"{_minutes:D2}:{_seconds:D2}";
    }

    public void Stop()
    {
        _endTime = DateTime.Now;
        var time = _endTime - _startTime;
        _seconds = time.Seconds;
        _minutes = time.Minutes;
        _isRunning = false;
    }

    public int GetTotalSeconds()
    {
        Stop();

        var time = _endTime - _startTime;
        var totalSeconds = time.TotalSeconds;

        return (int)totalSeconds;
    }
}