using System;

public sealed class Timer
{
    private readonly float _interval;
    private readonly Action _doOnLap;

    private bool _isStarted;
    private float _lastSpawnTimeLeft;

    public Timer(float interval, Action doOnLap)
    {
        _interval = interval;
        _doOnLap = doOnLap;
    }

    public void Start()
    {
        _isStarted = true;
    }

    public void Stop()
    {
        _isStarted = false;
    }

    private bool IsTimerExpired()
    {
        return _lastSpawnTimeLeft <= 0;
    }

    private void ReloadTimer()
    {
        _lastSpawnTimeLeft = _interval;
    }

    public void InvalidateLeftTime(float tick)
    {
        if (!_isStarted) return;
        _lastSpawnTimeLeft -= tick;
        if (!IsTimerExpired()) return;
        _doOnLap?.Invoke();
        ReloadTimer();
    }
}