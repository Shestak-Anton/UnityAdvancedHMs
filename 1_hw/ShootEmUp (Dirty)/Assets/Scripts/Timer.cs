using System;

public sealed class Timer
{
    private readonly float _interval;
    private readonly Action _doOnLap;
    
    public Timer(float interval, Action doOnLap)
    {
        _interval = interval;
        _doOnLap = doOnLap;
    }

    private float _lastSpawnTimeLeft;

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
        _lastSpawnTimeLeft -= tick;
        if (!IsTimerExpired()) return;
        _doOnLap?.Invoke();
        ReloadTimer();
    }
}