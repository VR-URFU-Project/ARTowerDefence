using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject gamingPlace;

    private void Start()
    {
        gamingPlace = GameObject.FindWithTag("GamingPlace");
    }
    
    private TowerData data;

    public void Purchase(string type)
    {
        switch (type)
        {
            case "Ballista":
                data = TowerManager.GetBallista();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;

            case "TreeHouse":
                data = TowerManager.GetTreeHouse();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;

            case "Mushroom":
                data = TowerManager.GetMushroom();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;

            case "LazerTower":
                data = TowerManager.GetLazerTower();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;
        }
    }

    public void CreateTower(Transform shape, Transform gamingPlace)
    {
        MoneySystem.ChangeMoney(-data.Price);
        var obj = Instantiate(data.prefab, shape.position, shape.rotation, gamingPlace);
        obj.GetComponent<Canon>().Tdata = this.data;
    }

    //public void PurchaseBallista()
    //{
    //    Debug.Log("Вы купили баллисту");
    //    Instantiate(ballistaShapePrefab, gamingPlace.transform);
    //}
}
