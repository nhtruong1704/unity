using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject gameObjectPanel;
    public Image image;
    public Sprite newSprite;

    Sprite oldSprite;

    public void Close()
    {
        gameObjectPanel.SetActive(false);
    }

    public void ChangeImage()
    {
        oldSprite = image.sprite;
        image.sprite = newSprite;
        newSprite = oldSprite;
    }
}
