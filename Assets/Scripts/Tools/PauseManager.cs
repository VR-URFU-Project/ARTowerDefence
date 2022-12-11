using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    private static int flag = 0;

    public static void TogglePause()
    {
        Time.timeScale = flag;
        if(flag == 1) GameTimer.ResumeTimer();
        else GameTimer.PauseTimer();
        flag = (flag == 0) ? 1 : 0;
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        GameTimer.PauseTimer();
        flag = 1;
    }

    public static void Resume()
    {
        Time.timeScale = 1;
        GameTimer.ResumeTimer();
        flag = 0;
    }
}
