using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseButtonBehavior : MonoBehaviour
{

    public void Pause()
    {
        TimescaleManager.Pause();
    }

    public void Resume()
    {
        TimescaleManager.Resume();
    }
}
