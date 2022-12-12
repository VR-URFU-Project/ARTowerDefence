using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    [SerializeField]
    string type;
    
    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>().Purchase(type);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
