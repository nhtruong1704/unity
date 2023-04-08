using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsScript : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject spherePrefab;
    public GameObject panelMenu;
    public GameObject spawnObject;
    bool isActive;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
           if(!isActive )
            {
                panelMenu.SetActive(true);
                isActive = true;
            }
            else
            {
                panelMenu.SetActive(false);
                isActive = false;
            }
        }
    }

    public void CreateBox()
    {
        Instantiate(boxPrefab, spawnObject.transform.position, spawnObject.transform.rotation);
    }

    public void CreateShere()
    {
        Instantiate(spherePrefab, spawnObject.transform.position, spawnObject.transform.rotation);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
