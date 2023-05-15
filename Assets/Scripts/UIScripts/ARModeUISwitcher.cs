using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARModeUISwitcher : MonoBehaviour
{
    [SerializeField] private GameObject AR_Tick;
    [SerializeField] private GameObject NoAR_Tick;
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            AR_Tick.SetActive(true);
            NoAR_Tick.SetActive(false);
        }
        else
        {
            AR_Tick.SetActive(false);
            NoAR_Tick.SetActive(true);
        }
    }
}
