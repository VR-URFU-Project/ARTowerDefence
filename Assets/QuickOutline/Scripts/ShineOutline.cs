using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineOutline : MonoBehaviour
{
    //const float toShine = 1f;
    //float toShineCounter = 1f;

    [SerializeField]
    float shiningSpeed = 10f;
    Outline rock_outline;

    bool shineOn = true;
    // OnEnable is called before the first frame update
    void Start()
    {
        rock_outline = this.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {/* 
        if(toShineCounter > 0 )
        {
            toShineCounter-= Time.deltaTime;
            rock_outline.OutlineWidth = 3f;
        }
        else
        {
            rock_outline.OutlineWidth = 0.5f;
            toShineCounter = toShine;
        } */
        if(shineOn)
        {
            if(rock_outline.OutlineWidth < 3f)
                rock_outline.OutlineWidth += shiningSpeed * 1 * Time.deltaTime;
            
            else
                shineOn = false;
        }
        else
        {
            if(rock_outline.OutlineWidth > 0f)
                rock_outline.OutlineWidth -= shiningSpeed * 1 * Time.deltaTime;
            else
                shineOn = true;
        }
    }

}
