using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StatsMenu : MonoBehaviour
{
    public static bool statsInPause = false;
    private bool onTable = false;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTable = false;
        }
    }

    void Update()
    {
        if (onTable && Input.GetKeyDown(KeyCode.F) && !AcertijosManager.acertijoInPause && !PauseMenu.GameIsPaused)
        {
            UpdateStats();
            Pause();
        }
    }

    void UpdateStats()
    {
        Transform stats = gameObject.transform.parent.Find("StatsMenu").Find("StatsMenuUI").Find("Stats").transform;

        stats.Find("PlayTime").Find("Data").GetComponent<TextMeshProUGUI>().text = (PlayerHealth.playedTime/3600).ToString("F2") + " horas";
        stats.Find("HistoryPercentage").Find("Data").GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name == "Fase-7" ? "50%" : "Desconocido";
        stats.Find("Deaths").Find("Data").GetComponent<TextMeshProUGUI>().text = PlayerHealth.numMuertes.ToString();
        
        stats.Find("KilledEnemies").Find("Data").GetComponent<TextMeshProUGUI>().text = PlayerHealth.numEnemigosEliminados.ToString();

        int solvedPuzzles = 0;
        if (Acertijo0.resolved) solvedPuzzles++;
        if (Acertijo1.resolved) solvedPuzzles++;
        if (Acertijo2.resolved) solvedPuzzles++;
        if (Acertijo3.resolved) solvedPuzzles++;
        if (Acertijo4.resolved) solvedPuzzles++;
        if (Acertijo5.resolved) solvedPuzzles++;
        if (Acertijo6.resolved) solvedPuzzles++;
        stats.Find("SolvedPuzzles").Find("Data").GetComponent<TextMeshProUGUI>().text = solvedPuzzles.ToString() + " de 7";

        stats.Find("CollectedCoins").Find("Data").GetComponent<TextMeshProUGUI>().text = SistemaMonedas.numMonedas.ToString() + " de 20";
    }

    void Pause()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameObject.transform.parent.Find("StatsMenu").gameObject.SetActive(true);
        statsInPause = true;

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
        gameObject.transform.parent.Find("StatsMenu").gameObject.SetActive(false);
        statsInPause = false;

        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = true;
        }
    }
}
