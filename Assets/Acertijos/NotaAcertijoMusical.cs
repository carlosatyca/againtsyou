using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NotaAcertijoMusical : MonoBehaviour
{
    private bool onNPC = false;
    public GameObject acertijoMenu;
    public GameObject player;

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
        if(onNPC && Input.GetKeyDown(KeyCode.F) && !AcertijosManager.acertijoInPause && !PauseMenu.GameIsPaused)
        {
            Pause();
        }
    }

    void Pause()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        acertijoMenu.SetActive(true);
        AcertijosManager.acertijoInPause = true;

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
}
