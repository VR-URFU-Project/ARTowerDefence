using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Sprite sprite;
    public Image image;

    public void DoChangeSprite()
    {
        image.sprite = sprite;
    }
}
