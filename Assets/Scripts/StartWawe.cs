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
    [SerializeField] GameObject mainArea;
    [SerializeField] GameObject VictoryPanel;

    private const int DISABLED_TIMER_VALUE = -10;
    private int curWave = 0;
    private int curSubWave = 0;
    private double timer = DISABLED_TIMER_VALUE;
    private Queue<SubwaveData> dataQueue;
    private int activeEnemies = 0;
    //private LinkedList<GameObject> activeEnemies;

    private int difficultyTime;
    private bool infinityMode;

    private float timeoutForSpawn = 0.2f;
    private int spawnBunchSize = 10000;

    private EnemySpawner enemySpawner;

    private void Start()
    {
        //mainArea = GameObject.FindGameObjectWithTag("GamingPlace");
        //activeEnemies = new LinkedList<GameObject>();
        enemySpawner = gameObject.GetComponent<EnemySpawner>();
        dataQueue = new Queue<SubwaveData>();
        VictoryPanel.SetActive(false);

        var reader = QSReader.Create("GameDifficulty");
        infinityMode = reader.Exists("infinityMode") ? reader.Read<bool>("infinityMode") : false;
        var difficulty = reader.Exists("difficulty") ? reader.Read<int>("difficulty") : 1;

        Debug.Log("difficulty: " + difficulty + " infinity mode: " + infinityMode);
        SwitchDifficulty(difficulty);
    }

    private void Update()
    {
        if (VictoryPanel.activeInHierarchy) return;
        if (activeEnemies == 0 && dataQueue.Count == 0 && WaveController.WavesTimeInfo[curWave] > difficultyTime && !infinityMode)
        {
            //Debug.Log("Победа");
            VictoryPanel.SetActive(true);
            TimescaleManager.Pause(true);
            return;
        }
        if (timer == DISABLED_TIMER_VALUE) return;
        timer -= Time.deltaTime;
        if (timer > 0) return;

        if (WaveController.WavesTimeInfo[curWave] > difficultyTime && !infinityMode) return;

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
        TimescaleManager.Resume(true);

        GenerateNextSubwave();
        processSubwave(dataQueue.Dequeue());
    }

    /// <summary>
    /// Вызывается при нажатии на кнопку в ui
    /// </summary>
    public void ContinueGameAfterWinning()
    {
        //GameTimer.ResumeTimer();
        TimescaleManager.Resume(true);

        infinityMode = true;

        GenerateNextSubwave();
        processSubwave(dataQueue.Dequeue());
    }

    private void GenerateNextSubwave()
    {
        if (infinityMode && curWave >= WaveController.WawesInfo.Count)
        {
            var wave = Random.Range(0, WaveController.WawesInfo.Count);
            curSubWave = Random.Range(0, WaveController.WawesInfo[wave].Data.Count);

            dataQueue.Enqueue(WaveController.WawesInfo[wave].Data[curSubWave]);
        }
        else
        {
            if (GameTimer.GetSeconds() >= WaveController.WavesTimeInfo[curWave])
            {
                ++curWave;
                curSubWave = 0;

                if (curWave >= WaveController.WawesInfo.Count && !infinityMode)
                {
                    timer = DISABLED_TIMER_VALUE;
                    return;
                }
            }
            dataQueue.Enqueue(WaveController.WawesInfo[curWave].Data[curSubWave]);
            curSubWave++;
            curSubWave = curSubWave % WaveController.WawesInfo[curWave].Data.Count;
        }
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
                StartCoroutine(SpawnCircle(subwave.Monsters));
                break;
            case "T":
                StartCoroutine(SpawnTogether(subwave.Monsters));
                break;
            case "R":
                StartCoroutine(SpawnRandom(subwave.Monsters));
                break;
        }
        timer = subwave.Duration;
    }

    /// <summary>
    /// Монстры появляются равномерно по кругу
    /// </summary>
    /// <param name="enemies"></param>
    private IEnumerator SpawnCircle(List<MonsterData> enemies)
    {
        var i = 0;
        for (; i < enemies.Count - enemies.Count % spawnPlaces.Count; ++i)
        {
            CreateEnemy(i % spawnPlaces.Count, enemies[i]);
            if (i % spawnBunchSize == 0 && i != 0)
                yield return new WaitForSeconds(timeoutForSpawn);
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
    private IEnumerator SpawnTogether(List<MonsterData> enemies)
    {
        var ind = getRandomSpawnPlace();
        for (var i = 0; i < enemies.Count; ++i)
        {
            CreateEnemy(ind, enemies[i]);
            if (i % spawnBunchSize == 0 && i != 0)
                yield return new WaitForSeconds(timeoutForSpawn);
        }

    }

    /// <summary>
    /// Монстры появляются в случайных местах
    /// </summary>
    /// <param name="enemies"></param>
    private IEnumerator SpawnRandom(List<MonsterData> enemies)
    {
        for (var i = 0; i < enemies.Count; ++i)
        {
            CreateEnemy(getRandomSpawnPlace(), enemies[i]);
            if (i % spawnBunchSize == 0 && i != 0)
                yield return new WaitForSeconds(timeoutForSpawn);
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
        var newData = MonsterController.GetMutatedEnemy(data);

        // задаём значения объекту в пуле
        enemySpawner.SetVariables(spawnPlaces[index].transform.position, newData, mainArea.transform);

        // спавним объект из пула
        EnemyScript newEnemy = null;
        switch (newData.Name)
        {
            case "Goblin":
                newEnemy = enemySpawner.GoblinPool.Get();
                break;
            case "Wolf":
                newEnemy = enemySpawner.WolfPool.Get();
                break;
            case "Orc":
                newEnemy = enemySpawner.OrcPool.Get();
                break;
            case "Harpy":
                newEnemy = enemySpawner.HarpyPool.Get();
                break;
        }

        if (newData.Name.Contains("_black") || newData.Name.Contains("_purple"))
        {
            newEnemy = Instantiate(newData.prefab, spawnPlaces[index].transform.position, new Quaternion(), mainArea.transform)
                .GetComponent<EnemyScript>();
        }

        //newEnemy.transform.localPosition = spawnPlaces[index].transform.localPosition; 
        if (newEnemy != null)
        {

            newEnemy/*.GetComponent<EnemyScript>()*/.SetTarget(crystal);
            newEnemy/*.GetComponent<EnemyScript>()*/.BasicData = newData;
            newEnemy.gameObject.SetActive(true);
            //activeEnemies.AddLast(newEnemy);
            ++activeEnemies;
            newEnemy.GetComponent<EnemyScript>().SetKillEvent(() =>
            {
                //MoneySystem.ChangeMoney(newData.Money);
                //activeEnemies.Remove(newEnemy);
                --activeEnemies;
            });
        }
    }

    public void SwitchDifficulty(int difficulty)
    {
        switch ((Difficulty)difficulty)
        {
            case Difficulty.easy:
                difficultyTime = 300;
                break;
            case Difficulty.normal:
                difficultyTime = 60;
                break;
            case Difficulty.hard:
                difficultyTime = 900;
                break;
        }
    }

    public void Save()
    {
        var writer = QuickSaveWriter.Create("GameStatus");
        writer.Write("subWave", curSubWave)
            .Write("wave", (curWave - 1 < 0) ? 0 : curWave - 1);
        writer.Commit();

        writer = QuickSaveWriter.Create("GameDifficulty");
        if (difficultyTime / 300 - 1 >= 0)
            writer.Write("difficulty", difficultyTime / 300 - 1);
        else
            writer.Write("difficulty", 0)
                .Write("infinityMode", infinityMode);
        writer.Commit();
    }

    public void Load()
    {
        var reader = QSReader.Create("GameStatus");
        curWave = reader.Exists("wave") ? reader.Read<int>("wave") : 0;
        curSubWave = reader.Exists("subWave") ? reader.Read<int>("subWave") : 0;

        //reader = QSReader.Create("GameDifficulty");
        //infinityMode = reader.Exists("infinityMode") ? reader.Read<bool>("infinityMode") : false;
        //var difficulty = reader.Exists("difficulty") ? reader.Read<int>("difficulty") : 1;
        //SwitchDifficulty(difficulty);
    }
}
