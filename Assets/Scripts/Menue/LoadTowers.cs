using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class LoadTowers : MonoBehaviour
{
    public void Load()
    {

        var reader = QSReader.Create("ToverData");
        var amount = reader.Read<int>("amount");
        for(int i=0; i<amount; i++)
        {
            var type = reader.Read<TowerType>("type" + i.ToString());
            TowerData data = null;
            switch (type)
            {
                case TowerType.Ballista:
                    data = TowerManager.GetBallista();
                    break;

                case TowerType.TreeHouse:
                    data = TowerManager.GetTreeHouse();
                    break;

                case TowerType.Mushroom:
                    data = TowerManager.GetMushroom();
                    break;

                case TowerType.LazerTower:
                    data = TowerManager.GetLazerTower();
                    break;
            }
            data.Health = reader.Read<int>("health" + i.ToString());
            var obj = Instantiate(data.prefab, gameObject.transform);          
            obj.transform.localPosition = reader.Read<Vector3>("position" + i.ToString());

            obj.GetComponent<TowerHealthLogic>().Tdata = data;
            obj.GetComponent<Canon>().Tdata = data;
        }
    }
}
