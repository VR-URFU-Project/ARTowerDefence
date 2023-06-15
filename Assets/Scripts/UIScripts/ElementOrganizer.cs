using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;

public class ElementOrganizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> elements;
    [SerializeField] private StatisticsCollector statisticsCollector;

    private Dictionary<TowerType, int> elementsDict;
    private List<KeyValuePair<TowerType, int>> sortedElements;

    private void OnEnable()
    {
        elementsDict = new Dictionary<TowerType, int>
        {
            { TowerType.Ballista, int.Parse(statisticsCollector.GetDamage(TowerType.Ballista)) },
            { TowerType.TreeHouse, int.Parse(statisticsCollector.GetDamage(TowerType.TreeHouse)) },
            { TowerType.Mushroom, int.Parse(statisticsCollector.GetDamage(TowerType.Mushroom)) },
            { TowerType.LazerTower, int.Parse(statisticsCollector.GetDamage(TowerType.LazerTower)) }
        };

        sortedElements = elementsDict.OrderByDescending(d => d.Value).ToList();

        SortElementsInHierarchy(sortedElements);
    }

    private void SortElementsInHierarchy(List<KeyValuePair<TowerType, int>> sortedElements)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].SetActive(false);
        }

        int index = 0;
        foreach(var pair in sortedElements)
        {
            var tempName = pair.Key.ToString();
            if (pair.Value > 0)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i].name.Contains(tempName))
                    {
                        elements[i].transform.SetSiblingIndex(index);
                        elements[i].SetActive(true);
                        index++;
                        break;
                    }
                }
            }
        }
    }
}
