using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveData
{
    public List<SubwaveData> Data = new List<SubwaveData>();
    public int SubwavesCount => Data.Count();

    public void AddSubwave(string description)
    {
        var subwave = new SubwaveData();
        var parts = description.Split('-').Select(x => x.Trim()).ToList();
        subwave.GenerateEnemies(parts[0]);
        subwave.SpawnType = parts[1];
        subwave.Duration = int.Parse(parts[2]);

        Data.Add(subwave);
    }
}
