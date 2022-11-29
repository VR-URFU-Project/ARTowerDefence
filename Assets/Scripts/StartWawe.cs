using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class StartWawe : MonoBehaviour
{
    //[SerializeField] GameObject EnemyPrefab;
    [SerializeField] List<GameObject> spawnPlaces;
    [SerializeField] GameObject crystal;
    [SerializeField] GameObject StartButton;
    //[SerializeField] GameObject AdditionalButton;

    private int waveCounter = 14;
    private double timer = 0;
    private Queue<SubwaveData> dataQueue = new Queue<SubwaveData>();
    private int activeEnemies = 0;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(EnableEnemy);
    }

    private void Update()
    {
        if(activeEnemies == 0 && dataQueue.Count == 0 /*&& AdditionalButton.gameObject.active  == false*/)
        {
            gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            gameObject.GetComponent<Button>().onClick.AddListener(EnableEnemy);
        }
        //AdditionalButton.gameObject.SetActive(activeEnemies == 0 && dataQueue.Count == 0);
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
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        gameObject.GetComponent<Button>().onClick.AddListener(PauseManager.TogglePause);
        //StartButton.GetComponent<Button>().enabled = false;
        //AdditionalButton.gameObject.SetActive(false);

        if (waveCounter >= 20) waveCounter = 0;
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
        var i = 0;
        for (; i < enemies.Count - enemies.Count % spawnPlaces.Count; ++i)
        {
            CreateEnemy(i % spawnPlaces.Count, enemies[i]);
        }

        var left = enemies.Count % spawnPlaces.Count;

        for(var ind = 0; ind< spawnPlaces.Count && left>0; i+=2, --left)
        {
            CreateEnemy(ind, enemies[i]);
            ++i;
        }

        for (var ind = 1; ind < spawnPlaces.Count && left > 0; ind += 2, --left)
        {
            CreateEnemy(ind, enemies[i]);
            ++i;
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
            CreateEnemy(ind, enemies[i]);
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
            CreateEnemy(getRandomSpawnPlace(), enemies[i]);
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

    /// <summary>
    /// Создаёт врага на месте спавна
    /// </summary>
    /// <param name="index">Индекс места спавна</param>
    private void CreateEnemy(int index, MonsterData data)
    {
        var newEnemy = Instantiate(data.prefab, spawnPlaces[index].transform);
        newEnemy.GetComponent<EnemyScript>().SetTarget(crystal);
        newEnemy.GetComponent<EnemyScript>().BasicData = data;
        newEnemy.gameObject.SetActive(true);
        ++activeEnemies;
        newEnemy.GetComponent<EnemyScript>().SetKillEvent(() => {
            MoneySystem.ChangeMoney(data.Money);
            --activeEnemies;
        });
    }
}
