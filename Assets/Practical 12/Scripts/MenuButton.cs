using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [System.Obsolete]
    public void StartScene()
    {
        Application.LoadLevel("Practical 12.2");
    }

    [System.Obsolete]
    public void BackScene()
    {
        Application.LoadLevel("Practical 12.1");
    }

    public void Options(GameObject window)
    {
        window.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
