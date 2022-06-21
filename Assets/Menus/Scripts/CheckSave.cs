using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CheckSave : MonoBehaviour
{
    void Update()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "saveData.cartom");
        if (!File.Exists(path)){
            GetComponent<Button>().interactable = false;
        } else
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
