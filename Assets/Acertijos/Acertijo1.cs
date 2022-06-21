using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Acertijo1 : MonoBehaviour
{
    private bool onNPC = false;
    public GameObject acertijoMenu;
    public GameObject player;
    public static bool resolved = false;
    public GameObject saveTextCanvas;
    public GameObject rightLimit;

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
        GameObject content = acertijoMenu.transform.Find("AcertijoMenuUI").Find("Contenido").gameObject;
        string result = "";

        for(int i = 1; i<=10; i++)
        {
            result += content.transform.Find("Opcion" + i.ToString()).Find("Text").GetComponent<TextMeshProUGUI>().text.ToString();
        }

        if (result == "2162018225")
        {
            resolved = true;
            SaveSystem.SavePlayer(player.GetComponent<PlayerHealth>());

            rightLimit.GetComponent<BoxCollider2D>().isTrigger = true;

            Resume();
            StartCoroutine(SaveText());
        }
        else
        {
            acertijoMenu.transform.Find("AcertijoMenuUI").Find("TextoError").gameObject.SetActive(true);
        }
    }

    public void UpNumber()
    {
        TMPro.TextMeshProUGUI textButton = EventSystem.current.currentSelectedGameObject.transform.parent.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        int result = int.Parse(textButton.text) + 1 == 10 ? 0 : (int.Parse(textButton.text) + 1);
        textButton.text = result.ToString();
    }

    public void DownNumber()
    {
        TMPro.TextMeshProUGUI textButton = EventSystem.current.currentSelectedGameObject.transform.parent.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        int result = int.Parse(textButton.text) - 1 == -1 ? 9 : (int.Parse(textButton.text) - 1);
        textButton.text = result.ToString();
    }
    IEnumerator SaveText()
    {
        saveTextCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        saveTextCanvas.SetActive(false);
    }
}
