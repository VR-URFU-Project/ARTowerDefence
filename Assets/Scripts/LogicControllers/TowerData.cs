using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerData
{
    public int Health { get; set; }

    public double Range { get; set; }

    public int Damage { get; set; }

    public double AtackSpeed { get; set; }

    public int TargetsAmount { get; set; }

    /// <summary>
    /// Может ли башня бить по воздуху
    /// </summary>
    public bool PVO_enabled { get; set; }

    public int Price { get; set; }

    public TowerType Type { get; set; }

    public GameObject prefab => Resources.Load<GameObject>(Enum.GetName(typeof(TowerType), Type));

    public GameObject shapePrefab => Resources.Load<GameObject>("shape_" + Enum.GetName(typeof(TowerType), Type));

    public TowerData()
    {

    }

    public TowerData(TowerData old)
    {
        Health = old.Health;
        Range = old.Range;
        Damage = old.Damage;
        AtackSpeed = old.AtackSpeed;
        TargetsAmount = old.TargetsAmount;
        PVO_enabled = old.PVO_enabled;
        Price = old.Price;
        Type = old.Type;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
