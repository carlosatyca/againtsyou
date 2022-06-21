using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Acertijo0 : MonoBehaviour
{
    public GameObject acertijoMenu;
    public static bool resolved = false;
    private string result = "";

    public void Pause()
    {
        acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").Find("Solucion").Find("Text").GetComponent<TMP_InputField>().ActivateInputField();
        Cursor.visible = true;
        //Time.timeScale = 0f;

        acertijoMenu.SetActive(true);
        AcertijosManager.acertijoInPause = true;
        acertijoMenu.transform.Find("AcertijoMenuUI").Find("TextoError").gameObject.SetActive(false);
    }

    public void Resume()
    {
        Cursor.visible = false;
        //Time.timeScale = 1f;
        acertijoMenu.SetActive(false);
        AcertijosManager.acertijoInPause = false;
    }

    public void checkResult()
    {
        result = acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").Find("Solucion").Find("Text").GetComponent<TMP_InputField>().text.ToString().ToLower();

        if (result == "he ido al bosque a por manzanas" || result == "he ido a por manzanas al bosque")
        {
            resolved = true;
            Resume();
        }
        else
        {
            acertijoMenu.transform.Find("AcertijoMenuUI").Find("TextoError").gameObject.SetActive(true);
        }
    }

    public void onChangeSolution()
    {
        string temp = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text.ToString();

        if(temp.Trim() != "" && temp[temp.Length-1].ToString() == " " && temp[temp.Length-1] == temp[temp.Length - 2])
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text = temp.Substring(0, temp.Length-1);
        }

        if (temp.Length >= 32) EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text = "";
    }
}
