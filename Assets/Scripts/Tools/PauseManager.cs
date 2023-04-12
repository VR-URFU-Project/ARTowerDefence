using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    private static float flag = 0;
    private static bool adminPaused = false;
    private static bool doubleSpeed = false;

    public static void TogglePause(bool admin = false)
    {
        //if (adminPaused && !admin) return;
        //Time.timeScale = flag;
        if (flag == 1)
        {
            //GameTimer.ResumeTimer();
            //adminPaused = false;
            Resume(admin);
        }
        else
        {
            //adminPaused = admin;
            // GameTimer.PauseTimer();
            Pause(admin);
        }
        //flag = (flag == 1) ? 0 : 1;
    }

    public static void Pause(bool admin = false)
    {
        if (adminPaused) return;
        adminPaused = admin;
        if (Math.Abs(Time.timeScale - 2) < 0.001) doubleSpeed = true;
        else doubleSpeed = false;
        Time.timeScale = 0;
        GameTimer.PauseTimer();
        flag = 1;
    }

    public static void Resume(bool admin = false)
    {
        if (adminPaused && !admin) return;
        adminPaused = false;
        if (doubleSpeed)
        {
            Time.timeScale = 2;
            doubleSpeed = false;
        }
        else
            Time.timeScale = 1;
        GameTimer.ResumeTimer();
        flag = 0;
    }
}
 