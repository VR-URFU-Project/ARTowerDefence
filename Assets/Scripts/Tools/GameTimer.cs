using System.Collections;
using System.Collections.Generic;
using System.Timers;

public static class GameTimer
{
    private static Timer timer;
    private static int seconds;

    public static void StartTimer()
    {
        timer = new Timer(1000);
        seconds = 0;
        timer.Elapsed += (sender, e) => { seconds++; };
        timer.Start();
    }

    public static void ResetTimer()
    {
        if (timer == null) return;
        timer.Stop();
        seconds = 0;
    }

    public static void PauseTimer()
    {
        if (timer == null) return;
        timer.Stop();
    }

    public static void ResumeTimer()
    {
        if (timer == null) return;
        timer.Start();
    }

    public static int GetSeconds()
    {
        return seconds;
    }
}
