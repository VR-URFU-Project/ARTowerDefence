 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные монстра
/// </summary>
public class MonsterData
{
    public string Name { get; set; }
    public int Health { get; set; }
    public float Movement { get; set; }
    public int Damage { get; set; }
    public float AttackSpeed { get; set; }
    public double AttacRange { get; set; }
    public bool Flight { get; set; }
    public int Money { get; set; }

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
        AttackSpeed = data.AttackSpeed;
        AttacRange = data.AttacRange;
        Flight = data.Flight;
        Money = data.Money;
    }
}
