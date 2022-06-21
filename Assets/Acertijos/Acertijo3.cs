using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Tilemaps;

public class Acertijo3 : MonoBehaviour
{
    private bool onNPC = false;
    public GameObject acertijoMenu;
    public GameObject player;
    public static bool resolved = false;
    private string result = "";
    public GameObject saveTextCanvas;
    public Grid grid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onNPC = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onNPC = false;
        }
    }

    void Update()
    {
        if(onNPC && Input.GetKeyDown(KeyCode.F) && !AcertijosManager.acertijoInPause && !resolved && !PauseMenu.GameIsPaused)
        {
            Pause();
            acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").Find("CodigoSolucion").Find("Solucion1").Find("Text").GetComponent<TMP_InputField>().ActivateInputField();
        } else if (onNPC && Input.GetKeyDown(KeyCode.F) && !AcertijosManager.acertijoInPause && !PauseMenu.GameIsPaused && resolved)
        {
            StartCoroutine(SaveText());
        }
    }

    void Pause()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        acertijoMenu.SetActive(true);
        AcertijosManager.acertijoInPause = true;
        acertijoMenu.transform.Find("AcertijoMenuUI").Find("TextoError").gameObject.SetActive(false);

        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = false;
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        acertijoMenu.SetActive(false);
        AcertijosManager.acertijoInPause = false;

        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = true;
        }
    }

    public void checkResult()
    {
        result = acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").Find("CodigoSolucion").Find("Solucion1").Find("Text").GetComponent<TMP_InputField>().text.ToString();
        result += acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").Find("CodigoSolucion").Find("Solucion2").Find("Text").GetComponent<TMP_InputField>().text.ToString();
        result += acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").Find("CodigoSolucion").Find("Solucion3").Find("Text").GetComponent<TMP_InputField>().text.ToString();

        if (result == "539")
        {
            resolved = true;
            SaveSystem.SavePlayer(player.GetComponent<PlayerHealth>());
            Resume();
            StartCoroutine(SaveText());
        }
        else
        {
            acertijoMenu.transform.Find("AcertijoMenuUI").Find("TextoError").gameObject.SetActive(true);
        }
    }

    public void onChangeSolution()
    {   
        string temp = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text.ToString();
        if (temp != "") EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text = temp[temp.Length - 1].ToString();
    }
    IEnumerator SaveText()
    {
        saveTextCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        saveTextCanvas.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        Tilemap t = grid.transform.Find("Fase5").GetComponent<Tilemap>();

        EraseWall1(t);
        yield return new WaitForSeconds(1f);
        EraseWall2(t);
    }

    void EraseWall1(Tilemap t)
    {
        t.SetTile(new Vector3Int(-46, -16, 0), null);
        t.SetTile(new Vector3Int(-46, -17, 0), null);
        t.SetTile(new Vector3Int(-46, -18, 0), null);
    }

    void EraseWall2(Tilemap t)
    {
        t.SetTile(new Vector3Int(-45, -16, 0), null);
        t.SetTile(new Vector3Int(-45, -17, 0), null);
        t.SetTile(new Vector3Int(-45, -18, 0), null);
    }
}
