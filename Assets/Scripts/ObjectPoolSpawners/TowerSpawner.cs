using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TowerSpawner : MonoBehaviour
{
    
    public List<GameObject> shapesPool;
    private int sizeOfShapesPool;
    public static TowerSpawner instance;

    private ObjectPool<Shape> shapePool;

    //private Shop shop;
    private GameObject tempGO;
    private Transform gamingPlace;
    private TowerData data;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        gamingPlace = GameObject.FindGameObjectWithTag("GamingPlace").transform;
        shapesPool = new List<GameObject>(sizeOfShapesPool);
        //FillList();

        //shapePool = new ObjectPool<Shape>(() =>
        //{
        //    foreach (var shape in shapesPool)
        //    {
        //        Instantiate(shape, gamingPlace);
        //    }, shape =>
        //    {

        //    }
        //});
    }

    //private void FillList(){
    //    // пресоздание голограммы баллисты
    //    tempGO = Instantiate(TowerManager.GetBallista().shapePrefab, gamingPlace);
    //    tempGO.SetActive(false);
    //    shapesPool.Add(tempGO);

    //    // пресоздание голограммы домика на дереве
    //    tempGO = Instantiate(TowerManager.GetTreeHouse().shapePrefab, gamingPlace);
    //    tempGO.SetActive(false);
    //    shapesPool.Add(tempGO);

    //    // пресоздание голограммы гриба
    //    tempGO = Instantiate(TowerManager.GetMushroom().shapePrefab, gamingPlace);
    //    tempGO.SetActive(false);
    //    shapesPool.Add(tempGO);

    //    // пресоздание голограммы лазерной башни
    //    tempGO = Instantiate(TowerManager.GetLazerTower().shapePrefab, gamingPlace);
    //    tempGO.SetActive(false);
    //    shapesPool.Add(tempGO);
    //}

    ///<summary>
    /// Активирует нужный Shape на поле
    ///</summary>
    public void GetTowerFromSpawner(TowerType type){
        switch(type){
            case TowerType.Ballista:
                shapesPool[0].SetActive(true);
                break;
            case TowerType.TreeHouse:
                shapesPool[1].SetActive(true);
                break;
            case TowerType.Mushroom:
                shapesPool[2].SetActive(true);
                break;
            case TowerType.LazerTower:
                shapesPool[3].SetActive(true);
                break;
        }
    }

    public void ResetSpawner()
    {
        foreach(var go in shapesPool)
        {
            
        }
    }

    // public void DisableShapeActiveStage(GameObject shape){
    //     switch(shape.name){
    //         case TowerType.Ballista:
    //             shapesPool[0].SetActive(false);
    //             break;
    //         case TowerType.TreeHouse:
    //             shapesPool[1].SetActive(false);
    //             break;
    //         case TowerType.Mushroom:
    //             shapesPool[2].SetActive(false);
    //             break;
    //         case TowerType.LazerTower:
    //             shapesPool[3].SetActive(false);
    //             break;
    //     }
    // }
}
