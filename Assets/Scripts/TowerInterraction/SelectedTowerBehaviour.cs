using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTowerBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject radius;
    [SerializeField]
    GameObject sphereRadius;

    private GameObject tower;

    public void SelectTower(GameObject tower)
    {
        this.tower = tower;
        if (tower.GetComponent<Canon>().Tdata.PVO_enabled)
        {
            sphereRadius.gameObject.transform.position = tower.gameObject.transform.position;
            sphereRadius.SetActive(true);
            radius.SetActive(false);
        }
        else
        {
            radius.gameObject.transform.position = tower.gameObject.transform.position;
            radius.SetActive(true);
            sphereRadius.SetActive(false);
        }
    }

    public void UpdateRadius()
    {
        var scale = 0.1f;
        var sphereScale = 0.017f;
        float range = (float)tower.GetComponent<Canon>().Tdata.Range;
        if (tower.GetComponent<Canon>().Tdata.PVO_enabled)
            sphereRadius.transform.localScale = new Vector3(range * sphereScale, range * sphereScale, range * sphereScale);
        else
            radius.transform.localScale = new Vector3(range * scale, 0.005f, range * scale);
    }

    public void DeselectTower()
    {
        if (tower.GetComponent<Canon>().Tdata.PVO_enabled)
            sphereRadius.SetActive(false);
        else
            radius.SetActive(false);
    }
}
