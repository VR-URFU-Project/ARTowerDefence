using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePos;
    [SerializeField] private GameObject originTower;

    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }

        //if (Input) 
    }

    void Update()
    {
        
    }
}
