using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitcher : MonoBehaviour, IPointerDownHandler
{
    //[SerializeField]
    //private bool _isOn = false;
    public bool isOn { get; set; }

    //[SerializeField] private RectTransform toggleIndicator;

    /*[SerializeField] RectTransform offValue;
    [SerializeField] RectTransform onValue;*/

    [SerializeField] GameObject on;
    [SerializeField] GameObject off;

    protected Toggle toggle;

    protected void OnEnable()
    {
        StartAction();
    }

    public virtual void StartAction()
    {
        toggle = GetComponent<Toggle>();
        isOn = toggle.isOn;
        MoveIndicator(isOn);
    }

    protected void MoveIndicator(bool value) 
    {
        if (value)
        {
            on.SetActive(true);
            off.SetActive(false);
            //toggleIndicator.position = onValue.position;
        }

        else
        {
            on.SetActive(false);
            off.SetActive(true);
            //toggleIndicator.position = offValue.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isOn = toggle.isOn;
        MoveIndicator(!isOn);
    }
}
