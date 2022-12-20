using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseButtonBehavior : MonoBehaviour
{

    public void Pause()
    {
        PauseManager.Pause();
    }

    public void Resume()
    {
        PauseManager.Resume();
    }
}
