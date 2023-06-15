using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.Audio;

public class OnGameStart : MonoBehaviour
{
    [SerializeField]
    AudioMixerGroup backMusic;

    [SerializeField]
    AudioMixerGroup effects;

    void Start()
    {
        TimescaleManager.Pause(true);
        HideObjects();
        SetMusic();
        LoadSystems();
    }

    private void SetMusic()
    {
        var reader = QSReader.Create("VolumeData");
        if (reader.Exists("CurrentBackVolume") && reader.Read<bool>("CurrentBackVolume"))
            backMusic.audioMixer.SetFloat("BackVolume", 0f);
        else 
            backMusic.audioMixer.SetFloat("BackVolume", -80f);

        reader = QSReader.Create("VolumeData");
        if (reader.Exists("CurrentEffectsVolume") && reader.Read<bool>("CurrentEffectsVolume"))
            effects.audioMixer.SetFloat("EffectsVolume", 0f);
        else 
            effects.audioMixer.SetFloat("EffectsVolume", -80f);
    }

    private void HideObjects()
    {
        var objects = GameObject.FindGameObjectsWithTag("HideOnGameLoad");
        foreach (var item in objects)
            item.SetActive(false);
    }

    private void LoadSystems()
    {
        GameTimer.ResetTimer();
        MoneySystem.BackToDefault();

        var reader = QSReader.Create("Temp");
        if (!reader.Exists("needsLoad")) return;
        var flag = reader.Read<int>("needsLoad");

        if (flag == 1)
        {
            GameDataController.LoadGameData();
            StatisticsCollector.LoadTowerStatistics();
        }
        else
        {
            StatisticsCollector.ResetTowersStatistics();
        }

        StatisticsCollector.LoadTimeStatistics();

        var writer = QuickSaveWriter.Create("Temp");
        writer.Write("needsLoad", 0);
        writer.Commit();
    }
}
