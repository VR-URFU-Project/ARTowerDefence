using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleModeSwitch : ToggleSwitcher
{
    // OnEnable is called before the first frame update
    override public void StartAction()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        toggle = GetComponent<Toggle>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
            isOn = true;
        else
            isOn = false;
        //Toggle(isOn);
        MoveIndicator(isOn);
    }

}
