using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShow : MonoBehaviour
{
    [SerializeField] Text MoneyText;
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = MoneySystem.GetMoney().ToString();
    }
}
