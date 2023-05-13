using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject gamingPlace;
    //private TowerSpawner towerSpawner;
    [SerializeField] private TMP_Text ballistaPrice;
    [SerializeField] private TMP_Text TreeHousePrice;
    [SerializeField] private TMP_Text mushroomPrice;
    [SerializeField] private TMP_Text lazerTowerPrice;

    private void Start()
    {
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        //towerSpawner = new TowerSpawner();
        ballistaPrice.text = TowerManager.GetBallista().Price.ToString();
        TreeHousePrice.text = TowerManager.GetTreeHouse().Price.ToString();
        mushroomPrice.text = TowerManager.GetMushroom().Price.ToString();
        lazerTowerPrice.text = TowerManager.GetLazerTower().Price.ToString();
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
                    //towerSpawner.GetTowerFromSpawner(data.Type);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;

            case "TreeHouse":
                data = TowerManager.GetTreeHouse();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                    //towerSpawner.GetTowerFromSpawner(data.Type);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;

            case "Mushroom":
                data = TowerManager.GetMushroom();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                    //towerSpawner.GetTowerFromSpawner(data.Type);
                else
                    CameraHandler.ChangeShopItemSelectedStage(false);
                break;

            case "LazerTower":
                data = TowerManager.GetLazerTower();
                if (MoneySystem.GetMoney() >= data.Price)
                    Instantiate(data.shapePrefab, gamingPlace.transform);
                    //towerSpawner.GetTowerFromSpawner(data.Type);
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
    //    Debug.Log("�� ������ ��������");
    //    Instantiate(ballistaShapePrefab, gamingPlace.transform);
    //}
}
