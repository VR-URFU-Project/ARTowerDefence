using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Start : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] List<GameObject> spawnPlaces;
    [SerializeField] GameObject crystal;

    private int waveCounter = 6;
    private double timer = 0;
    private Queue<SubwaveData> dataQueue = new Queue<SubwaveData>();

    private void Update()
    {
        if (timer == 0) return;
        timer -= Time.deltaTime;
        if (timer > 0) return;
        processSubwave(dataQueue.Dequeue());
    }

    /// <summary>
    /// Вызывается при нажатии на кнопку в ui
    /// </summary>
    public void EnableEnemy()
    {
        foreach (var subWave in WaveController.WawesInfo[waveCounter].Data)
        {
            dataQueue.Enqueue(subWave);
        }
        processSubwave(dataQueue.Dequeue());
        ++waveCounter;
    }

    /// <summary>
    /// Создаёт подволну на сцене
    /// </summary>
    /// <param name="subwave"></param>
    private void processSubwave(SubwaveData subwave)
    {
        switch (subwave.SpawnType)
        {
            case "C":
                SpawnCircle(subwave.Monsters);
                break;
            case "T":
                SpawnTogether(subwave.Monsters);
                break;
            case "R":
                SpawnRandom(subwave.Monsters);
                break;
        }

        if (dataQueue.Count == 0)
        {
            timer = 0;
            return;
        }
        timer = subwave.Duration;
    }

    /// <summary>
    /// Монстры появляются равномерно по кругу
    /// </summary>
    /// <param name="enemies"></param>
    private void SpawnCircle(List<MonsterData> enemies)
    {
        for (var i = 0; i < enemies.Count; ++i)
        {
            var newEnemy = GameObject.Instantiate(EnemyPrefab, spawnPlaces[i % spawnPlaces.Count].transform);
            newEnemy.GetComponent<EnemyScript>().SetTarget(crystal);
            newEnemy.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Все монстры появляются в одном случайном месте
    /// </summary>
    /// <param name="enemies"></param>
    private void SpawnTogether(List<MonsterData> enemies)
    {
        var ind = getRandomSpawnPlace();
        for (var i = 0; i < enemies.Count; ++i)
        {
            var newEnemy = GameObject.Instantiate(EnemyPrefab, spawnPlaces[ind].transform);
            newEnemy.GetComponent<EnemyScript>().SetTarget(crystal);
            newEnemy.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Монстры появляются в случайных местах
    /// </summary>
    /// <param name="enemies"></param>
    private void SpawnRandom(List<MonsterData> enemies)
    {
        for (var i = 0; i < enemies.Count; ++i)
        {
            var newEnemy = GameObject.Instantiate(EnemyPrefab, spawnPlaces[getRandomSpawnPlace()].transform);
            newEnemy.GetComponent<EnemyScript>().SetTarget(crystal);
            newEnemy.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Возвращает индекс случайного места спавна
    /// </summary>
    /// <returns>индекс места спавна</returns>
    private int getRandomSpawnPlace()
    {
        var ind = Random.Range(0, spawnPlaces.Count);
        if (ind == spawnPlaces.Count) --ind;
        return ind;
    }
}
