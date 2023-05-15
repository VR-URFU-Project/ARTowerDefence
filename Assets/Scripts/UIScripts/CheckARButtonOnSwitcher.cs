using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckARButtonOnSwitcher : MonoBehaviour
{
    private void OnEnable()
    {
        var reader = QSReader.Create("supportAR");
        var amount = reader.Read<int>("supportAR");

        if(amount == 1)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

}
