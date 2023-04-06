using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public TextMeshProUGUI userName;

    public static void Create(GameObject prefab, string userName, Transform parent)
    {
        var ob =Instantiate(prefab, parent);
        Notification join = ob.GetComponent<Notification>();
        join.userName.text = userName;
    }
}
