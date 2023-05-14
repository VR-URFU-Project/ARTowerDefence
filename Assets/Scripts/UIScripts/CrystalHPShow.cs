using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalHPShow : MonoBehaviour
{
    private TowerHealthLogic health;
    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Crystal").GetComponent<TowerHealthLogic>();
    }

    void Update()
    {
        if (health.Tdata != null)
            gameObject.GetComponent<TMP_Text>().text = health.Tdata.Health.ToString();
    }
}
