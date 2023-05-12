using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class TowerData
{
    public int Level { get; set; }

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

    public double ProjectileSpeed { get; set; }

    public int UpdatePrice => (Price * 3 / 2) * Level;

    public int SellPrice => Price * (4 + 3 * (Level * Level - Level)) / 8;

    public GameObject prefab => Resources.Load<GameObject>(Enum.GetName(typeof(TowerType), Type));

    public GameObject shapePrefab => Resources.Load<GameObject>("shape_" + Enum.GetName(typeof(TowerType), Type));

    public TowerData()
    {
        Level = 1;
    }

    public TowerData(TowerData old)
    {
        Level = old.Level;
        Health = old.Health;
        Range = old.Range;
        Damage = old.Damage;
        AtackSpeed = old.AtackSpeed;
        TargetsAmount = old.TargetsAmount;
        PVO_enabled = old.PVO_enabled;
        Price = old.Price;
        Type = old.Type;
        ProjectileSpeed = old.ProjectileSpeed;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Upgrade()
    {
        var updateInfo = UpdateController.GetUpdateString(Type, Level);
        if (updateInfo is null)
            return;        
        Level++;
        var parts = updateInfo.Split('|').Select(x => x.Trim());

        foreach(var part in parts)
        {
            var data = part.Split(' ').ToArray();
            switch (data[1])
            {
                case var str when str.Contains("hp"):
                    Health += int.Parse(data[0]);
                    break;
                case var str when str.Contains("dmg"):
                    Damage += int.Parse(data[0]);
                    break;
                case var str when str.Contains("range"):
                    Range += double.Parse(data[0], CultureInfo.InvariantCulture);
                    break;
                case var str when str.Contains("spd"):
                    AtackSpeed += double.Parse(data[0], CultureInfo.InvariantCulture);
                    break;
                case var str when str.Contains("trgt"):
                   // и как?
                    break;

            }
        }
    }
}
