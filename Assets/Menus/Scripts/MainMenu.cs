using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        SaveSystem.buttonPressed = "NewGame";
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
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
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
