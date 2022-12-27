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

    [SerializeField] private RectTransform toggleIndicator;

    [SerializeField] RectTransform offValue;
    [SerializeField] RectTransform onValue;

    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        isOn = toggle.isOn;
        //Toggle(isOn);
        MoveIndicator(isOn);
    }


    //private void Toggle(bool value)
    //{
    //    if (value != isOn)
    //    {
    //        isOn = value;

    //        MoveIndicator(isOn);
    //    }

    //}

    private void MoveIndicator(bool value) 
    {
        if (value)
            toggleIndicator.position = onValue.position;
        else
            toggleIndicator.position = offValue.position;
    }

    //public void OnSwitch(PointerEventData eventData)
    //{
    //    Toggle(!isOn);
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        isOn = toggle.isOn;
        //Toggle(!isOn);
        MoveIndicator(!isOn);
    }
}
