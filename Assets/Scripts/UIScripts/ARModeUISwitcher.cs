using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARModeUISwitcher : MonoBehaviour
{
    [SerializeField] private GameObject AR_Tick;
    [SerializeField] private GameObject NoAR_Tick;
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            AR_Tick.SetActive(true);
            AR_Tick.transform.parent.gameObject.GetComponent<Button>().interactable = false;
            
            NoAR_Tick.SetActive(false);
            NoAR_Tick.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            AR_Tick.SetActive(false);
            AR_Tick.transform.parent.gameObject.GetComponent<Button>().interactable = true;

            NoAR_Tick.SetActive(true);
            NoAR_Tick.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
