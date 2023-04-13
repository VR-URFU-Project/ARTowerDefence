using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimescaleManager
{
    private static float pauseFlag = 0;
    private static bool adminPaused = false;
    private static float speedBeforePause = 1f;

    public static void TogglePause(bool admin = false)
    {
        if (pauseFlag == 1)
            Resume(admin);
        else
            Pause(admin);
    }

    public static void Pause(bool admin = false)
    {
        if (adminPaused && !admin) return;
        adminPaused = admin;
        speedBeforePause = (Time.timeScale > 0.2f) ? Time.timeScale : 1f;
        Time.timeScale = 0;
        GameTimer.PauseTimer();
        pauseFlag = 1;
    }

    public static void Resume(bool admin = false)
    {
        if (adminPaused && !admin) return;
        adminPaused = false;
        Time.timeScale = speedBeforePause;
        GameTimer.ResumeTimer();
        pauseFlag = 0;
    }

    public static void ScaleTime(float scale)
    {
        if (pauseFlag == 1)
        {
            speedBeforePause = scale;
            GameTimer.SetTimer((int)(1000 / scale));
        }
        else
        {
            Time.timeScale = scale;
            GameTimer.StartTimer((int)(1000 / scale));
        }
    }
}
 