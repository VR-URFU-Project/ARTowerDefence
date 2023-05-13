using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool<EnemyScript> enemyPool;

    private Transform spawnPlace;
    private MonsterData monsterData;
    private Transform mainArea;
    private Vector3 coordinates;

    private void Start()
    {
        enemyPool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemy, 
            true, defaultCapacity: 1000, maxSize: 2000);
    }

    /// <summary>
    /// Задать нужные переменные перед спавном врагов
    /// </summary>
    public void SetVariables(Vector3 coordinates, MonsterData monsterData, Transform mainArea)
    {
        this.coordinates = coordinates;
        this.monsterData = monsterData;
        this.mainArea = mainArea;
    }

/*    public void SetPoolFromSpawner(EnemyScript enemyScript)
    {
        enemyScript.SetPool(enemyPool);
    }*/

    private EnemyScript CreateEnemy()
    {
        var enemyObject = Instantiate(
                                        monsterData.prefab, coordinates, new Quaternion(), mainArea);
        var enemyScript = enemyObject.GetComponent<EnemyScript>();

        //SetPoolFromSpawner(enemyScript);
        enemyScript.SetPool(enemyPool);

        return enemyScript;
    }

    private void OnTakeEnemyFromPool(EnemyScript enemyScript)
    {
        enemyScript.gameObject.transform.parent = mainArea;
        enemyScript.gameObject.transform.position = coordinates;

        enemyScript.gameObject.SetActive(true);
    }

    private void OnReturnEnemyToPool(EnemyScript enemyScript)
    {
        enemyScript.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(EnemyScript enemyScript)
    {
        Destroy(enemyScript.gameObject);
    }
}
