using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    [SerializeField] private GameObject originTower;
    private GameObject gamingPlace;

    void Start()
    {
        gamingPlace = GameObject.FindWithTag("GamingPlace");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Plane")
            transform.position = hit.point;
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Plane")
                transform.position = hit.point;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(originTower, transform.position, transform.rotation, gamingPlace.transform);
            Destroy(gameObject);
        }
    }
}
