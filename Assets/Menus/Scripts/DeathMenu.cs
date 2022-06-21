using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void ExitMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ContinueGame()
    {
        Cursor.visible = true;
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            SaveSystem.buttonPressed = "LoadGame";
            Cursor.visible = false;
            SceneManager.LoadScene(data.level);
        }
        else
        {
            Debug.LogError("Error cargando la partida guardada");
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
