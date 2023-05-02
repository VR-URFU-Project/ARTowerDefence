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

    private void Start()
    {
        enemyPool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemy, 
            true, defaultCapacity: 10000, maxSize: 50000);
    }

    /// <summary>
    /// «адать нужные переменные перед спавном врагов
    /// </summary>
    public void SetVariables(Transform spawnPlace, MonsterData monsterData, Transform mainArea)
    {
        this.spawnPlace = spawnPlace;
        this.monsterData = monsterData;
        this.mainArea = mainArea;
    }

    private EnemyScript CreateEnemy()
    {
        var enemyObject = Instantiate(
                                        monsterData.prefab, spawnPlace.position, new Quaternion(), mainArea);
        var enemyScript = enemyObject.GetComponent<EnemyScript>();

        enemyScript.SetPool(enemyPool);
        
        return enemyScript;
    }

    private void OnTakeEnemyFromPool(EnemyScript enemyScript)
    {
        enemyScript.gameObject.transform.parent = mainArea;
        enemyScript.gameObject.transform.position = spawnPlace.position;

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
