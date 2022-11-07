using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneySystem
{
    private static int money = 0;

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
}
