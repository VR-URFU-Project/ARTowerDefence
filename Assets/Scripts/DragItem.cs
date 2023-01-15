using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    [SerializeField]
    string type;
    private TowerData data;
    private GameObject gamingPlace;
    [SerializeField]
    private Transform spawnPlace;

    private void Start()
    {
        gamingPlace = GameObject.FindWithTag("GamingPlace");
    }

    //private void OnMouseUpAsButton()
    //{
    //    data = TowerManager.GetBallista();
    //    if (MoneySystem.GetMoney() >= data.Price)
    //        Instantiate(data.shapePrefab,
    //            spawnPlace.position,
    //            spawnPlace.rotation,
    //            gamingPlace.transform);
    //}

    //private void OnMouseDrag()
    //{
    //    GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>().Purchase(type);
    //}
}
