using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Messenger : MonoBehaviour
{
    public TextMeshProUGUI userName;
    public TextMeshProUGUI messengerText;

    public static void Create(GameObject prefab ,string userName, string messenger, Transform parent, string currentName)
    {
        GameObject mesGO = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        Messenger mess = mesGO.GetComponent<Messenger>();
        mess.userName.text = userName;
        if(userName == currentName)
            mess.userName.color = Color.red;
        mess.messengerText.text = messenger;
    }


}
