using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
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

    public string PrefabName { get; set; }

    public string ShapePrefabName { get; set; }

    public GameObject prefab => Resources.Load<GameObject>(PrefabName);

    public GameObject shapePrefab => Resources.Load<GameObject>(ShapePrefabName);
}
