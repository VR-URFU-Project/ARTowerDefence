using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public static class MoneySystem
{
    private const int DEFAULT_MONEY = 100;
    private static int money = DEFAULT_MONEY;

    /// <summary>
    /// Получить количество денег
    /// </summary>
    public static int GetMoney()
    {
        return money;
    }

    /// <summary>
    /// Изменение количества денег
    /// </summary>
    /// <param name="value">Количество, на которое меняем</param>
    /// <returns>Если изменение невозможно, вернёт false, иначе true</returns>
    public static bool ChangeMoney(int value)
    {
        if (money + value < 0)
            return false;
        money += value;
        return true;
    }

    public static void BackToDefault()
    {
        money = DEFAULT_MONEY;
    }

    public static void Save()
    {
        var writer = QuickSaveWriter.Create("PlayerMoney");
        writer.Write("money", money);
        writer.Commit();
    }

    public static void Load()
    {
        var reader = QSReader.Create("PlayerMoney");
        money = reader.Exists("money") ? reader.Read<int>("money") : 0;
    }
}
