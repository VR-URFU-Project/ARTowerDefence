using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит информацию о волнах
/// </summary>
public static class WaveController
{
    public static List<WaveData> WawesInfo = CSVReader.ReadWaveData();

    public static List<int> WavesTimeInfo = CSVReader.ReadWaveTimeData();

    public static void RefreshWaveData()
    {
        WawesInfo = CSVReader.ReadWaveData();
        WavesTimeInfo = CSVReader.ReadWaveTimeData();
    }
}
