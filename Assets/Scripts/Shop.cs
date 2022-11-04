using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //BuildManager buildManager;
    public GameObject ballistaShapePrefab;
    private GameObject gamingPlace;

    private void Start()
    {
        //buildManager = BuildManager.instance;
        gamingPlace = GameObject.FindWithTag("GamingPlace");
    }

    public void PurchaseBallista()
    {
        Debug.Log("Вы купили баллисту");
        //buildManager.SetTowerToBuild(buildManager.canonPrefab);
        Instantiate(ballistaShapePrefab, gamingPlace.transform);
    }

    public void PurchaseAirCanon()
    {
        Debug.Log("Вы купили улучшенную баллисту");
        //buildManager.SetTowerToBuild(buildManager.airCanonPrefab);
    }
}
