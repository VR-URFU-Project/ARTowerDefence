using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
