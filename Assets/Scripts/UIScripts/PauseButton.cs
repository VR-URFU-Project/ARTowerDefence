using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            gameObject.GetComponentsInChildren<Image>()[0].sprite = Resources.Load<Sprite>("Play");
            SpriteState spriteState = new SpriteState();
            spriteState = gameObject.GetComponent<Button>().spriteState;
            spriteState.pressedSprite = Resources.Load<Sprite>("PlayD");
            //gameObject.GetComponentsInChildren<Image>()[0].SetNativeSize();
        }

        else
        {
            gameObject.GetComponentsInChildren<Image>()[0].sprite = Resources.Load<Sprite>("Pause");
            SpriteState spriteState = new SpriteState();
            spriteState = gameObject.GetComponent<Button>().spriteState;
            spriteState.pressedSprite = Resources.Load<Sprite>("PauseD");
            //gameObject.GetComponentsInChildren<Image>()[0].SetNativeSize();
        }
            
    }

    /// <summary>
    /// При нажатии на кнопку
    /// </summary>
    public void PressButton()
    {
        TimescaleManager.TogglePause(true);
        //gameObject.GetComponentInChildren<Text>().text = (Time.timeScale == 0) ? "Resume" : "Pause";
    }
}
