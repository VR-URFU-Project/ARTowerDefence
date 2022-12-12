using System.Collections;
using System.Collections.Generic;
using System.Timers;
using CI.QuickSave;

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

    public static string GetFormatedTime()
    {
        int min = seconds / 60;
        int sec = seconds % 60;
        string formated = ((min < 10) ? "0" + min.ToString() : min.ToString()) +
                    ":" +
                    ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
        return formated;
    }

    public static void Save() {
        var writer = QuickSaveWriter.Create("PlayerTime");
        writer.Write("seconds", seconds);
        writer.Commit();
    }

    public static void Load() {
        var reader = QSReader.Create("PlayerTime");
        seconds = reader.Exists("seconds") ? reader.Read<int>("seconds") : 0;
    }
}
