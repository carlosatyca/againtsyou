using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public GameObject blurPanel;
    public GameObject player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !AcertijosManager.acertijoInPause && !StatsMenu.statsInPause)
        {
            if (GameIsPaused && pauseMenuUI.activeSelf)
            {
                Resume();
            } 
            else if (GameIsPaused && controlsMenuUI.activeSelf)
            {
                ExitControlsMenu();
            } 
            else
            {
                Pause();
            }

        }

    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        blurPanel.SetActive(false);
        Time.timeScale = 1f;

        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = true;
        }
        Cursor.visible = false;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        blurPanel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach(MonoBehaviour script in playerScripts)
        {
            script.enabled = false;
        }
        GameIsPaused = true;
    }

    public void LoadControlsMenu()
    {
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }
    public void ExitControlsMenu()
    {
        controlsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ExitMainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = true;
        //pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");
    }
}
