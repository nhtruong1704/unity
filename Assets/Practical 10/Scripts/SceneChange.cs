using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [System.Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Application.LoadLevel("Practical 10.1");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("Practical 10.2");
        }
    }
}
