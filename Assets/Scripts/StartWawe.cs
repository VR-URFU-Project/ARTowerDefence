using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using CI.QuickSave;

public class StartWawe : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPlaces;
    [SerializeField] GameObject crystal;
    //[SerializeField] GameObject StartButton;

    private const int DISABLED_TIMER_VALUE = -10;
    private int curWave = 0;
    private int curSubWave = 0;
    private double timer = DISABLED_TIMER_VALUE;
    private Queue<SubwaveData> dataQueue = new Queue<SubwaveData>();
    private int activeEnemies = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if (activeEnemies == 0 && dataQueue.Count == 0 && curWave >= WaveController.WawesInfo.Count)
        {
            Debug.Log("Победа");
            return;
        }
        if (timer == DISABLED_TIMER_VALUE) return;
        timer -= Time.deltaTime;
        if (timer > 0) return;

        GenerateNextSubwave();
        if (timer == DISABLED_TIMER_VALUE) return;
        processSubwave(dataQueue.Dequeue());
    }

    /// <summary>
    /// Вызывается при нажатии на кнопку в ui
    /// </summary>
    public void EnableEnemy()
    {
        GameTimer.StartTimer();
        PauseManager.Resume(true);

        GenerateNextSubwave();
        processSubwave(dataQueue.Dequeue());
    }

    private void GenerateNextSubwave()
    {
        if(GameTimer.GetSeconds() >= WaveController.WavesTimeInfo[curWave])
        {
            ++curWave;
            curSubWave = 0;

            if (curWave >= WaveController.WawesInfo.Count)
            {
                timer = DISABLED_TIMER_VALUE;
                return;
            }
        }
        dataQueue.Enqueue(WaveController.WawesInfo[curWave].Data[curSubWave]);
        curSubWave++;
        curSubWave = curSubWave % WaveController.WawesInfo[curWave].Data.Count;
    }

    /// <summary>
    /// Создаёт подволну на сцене
    /// </summary>
    /// <param name="subwave"></param>
    private void processSubwave(SubwaveData subwave)
    {
        Debug.Log("Processing " + GameTimer.GetSeconds().ToString());
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

        for (var ind = 0; ind < spawnPlaces.Count && left > 0; ind += 2, --left)
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
        newEnemy.GetComponent<EnemyScript>().BasicData = new MonsterData(data);
        newEnemy.gameObject.SetActive(true);
        ++activeEnemies;
        newEnemy.GetComponent<EnemyScript>().SetKillEvent(() =>
        {
            MoneySystem.ChangeMoney(data.Money);
            --activeEnemies;
        });
    }

    public void Save()
    {
        var writer = QuickSaveWriter.Create("GameStatus");
        writer.Write("subWave", curSubWave)
            .Write("wave", (curWave-1 < 0) ? 0 : curWave - 1);
        writer.Commit();
    }

    public void Load()
    {
        var reader = QSReader.Create("GameStatus");
        curWave = reader.Exists("wave") ? reader.Read<int>("wave") : 0;
        curSubWave = reader.Exists("subWave") ? reader.Read<int>("subWave") : 0;
    }
}
