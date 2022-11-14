using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    private static int flag = 0;

    public static void TogglePause()
    {
        Time.timeScale = flag;
        flag = (flag == 0) ? 1 : 0;
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        flag = 1;
    }

    public static void Resume()
    {
        Time.timeScale = 1;
        flag = 0;
    }

}
