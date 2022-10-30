using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Start : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] List<GameObject> spawnPlaces;
    [SerializeField] GameObject crystal;
    private int ofset = 0;
    private int waveCounter=0;

    public void EnableEnemy()
    {
        foreach(var subWave in WaveController.WawesInfo[waveCounter].Data)
        {
            for (var i = 0; i < subWave.Monsters.Count; ++i)
            {
                var newEnemy = GameObject.Instantiate(EnemyPrefab, spawnPlaces[i % spawnPlaces.Count].transform);
                newEnemy.GetComponent<EnemyScript>().SetTarget(crystal);
                newEnemy.gameObject.SetActive(true);
            }
            Thread.Sleep(subWave.Duration * 1000);
        }
        ++waveCounter;
    }

    //public void EnableEnemy()
    //{
    //    for (var i = 0; i < spawnPlaces.Count - 1; i+=2)
    //    {
    //       var newEnemy = GameObject.Instantiate(EnemyPrefab, spawnPlaces[i + ofset].transform);
    //        newEnemy.GetComponent<EnemyScript>().SetTarget(crystal);
    //        newEnemy.gameObject.SetActive(true);
    //    }
    //    ofset =  ofset == 1 ? 0 : 1;
    //}
}
