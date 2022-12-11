 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные монстра
/// </summary>
public class MonsterData
{
    public string Name;
    public int Health;
    public float Movement;
    public int Damage;
    public float AttacSpeed;
    public double AttacRange;
    public bool Flight;
    public int Money;

    //public string PrefabName;
    public GameObject prefab => Resources.Load<GameObject>(Name);

    public MonsterData()
    {

    }

    public MonsterData(MonsterData data)
    {
        Name = data.Name;
        Health = data.Health;
        Movement = data.Movement;
        Damage = data.Damage;
        AttacSpeed = data.AttacSpeed;
        AttacRange = data.AttacRange;
        Flight = data.Flight;
        Money = data.Money;
    }
}
