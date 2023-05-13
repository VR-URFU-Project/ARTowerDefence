using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNotIngameObjects : MonoBehaviour
{
    // OnEnable is called before the first frame update
    void Start()
    {
        var objects = GameObject.FindGameObjectsWithTag("HideOnGameLoad");
        foreach(var item in objects)
        {
            item.SetActive(false);
        }
    }
}
