using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShow : MonoBehaviour
{
    [SerializeField] TMP_Text MoneyText;
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = MoneySystem.GetMoney().ToString();
    }
}
