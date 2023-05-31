using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool<EnemyScript> GoblinPool;
    public ObjectPool<EnemyScript> WolfPool;
    public ObjectPool<EnemyScript> OrcPool;
    public ObjectPool<EnemyScript> HarpyPool;

    private Transform spawnPlace;
    private MonsterData monsterData;
    private Transform mainArea;
    private Vector3 coordinates;

    private void Start()
    {
        GoblinPool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemy, 
            true, defaultCapacity: 1000, maxSize: 2000);

        WolfPool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemy,
            true, defaultCapacity: 1000, maxSize: 2000);

        OrcPool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemy,
            true, defaultCapacity: 1000, maxSize: 2000);

        HarpyPool = new ObjectPool<EnemyScript>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemy,
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

    private EnemyScript CreateEnemy()
    {
        var enemyObject = Instantiate(
                                        monsterData.prefab, coordinates, new Quaternion(), mainArea);
        var enemyScript = enemyObject.GetComponent<EnemyScript>();

        // неактивно из-за того, что в EnemyScript пул в комментарии
        switch (monsterData.Name)
        {
            case "Goblin":
                enemyScript.SetPool(GoblinPool);
                break;
            case "Wolf":
                enemyScript.SetPool(WolfPool);
                break;
            case "Orc":
                enemyScript.SetPool(OrcPool);
                break;
            case "Harpy":
                enemyScript.SetPool(HarpyPool);
                break;
        }

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
