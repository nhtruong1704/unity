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
        MeshRenderer m = Instantiate(boxPrefab, spawnObject.transform.position, spawnObject.transform.rotation).GetComponent<MeshRenderer>();
        m.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void CreateShere()
    {
        MeshRenderer m = Instantiate(spherePrefab, spawnObject.transform.position, spawnObject.transform.rotation).GetComponent<MeshRenderer>();
        m.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
