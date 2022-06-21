using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionFases : MonoBehaviour
{
    public GameObject transicionFases;
    public GameObject player;
    public GameObject pauseMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            string actualLevel = SceneManager.GetActiveScene().name;
            int actualIndexLevel = SceneManager.GetActiveScene().buildIndex;

            if (actualLevel == "Fase-1")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-2")
            {
                float alturaPlayer = player.transform.localPosition.y;
                if (alturaPlayer >= 0f) {
                    int nextLevel = actualIndexLevel + 2;
                    transitionLevel(collision, nextLevel);
                }
                else if (alturaPlayer >= -9f && alturaPlayer <= -4f)
                {
                    int nextLevel = actualIndexLevel + 3;
                    transitionLevel(collision, nextLevel);
                }
                else if (alturaPlayer <= -13f)
                {
                    int nextLevel = actualIndexLevel + 1;
                    transitionLevel(collision, nextLevel);
                }
            }
            else if (actualLevel == "Fase-3")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-4")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-5")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-5.1")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-5.2")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-7")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-6")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-6.1")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-6.2")
            {
                float alturaPlayer = player.transform.localPosition.y;
                if (alturaPlayer >= -40f)
                {
                    int nextLevel = actualIndexLevel + 1;
                    transitionLevel(collision, nextLevel);
                }
                else if (alturaPlayer >= -51f && alturaPlayer <= -43f)
                {
                    int nextLevel = actualIndexLevel + 2;
                    transitionLevel(collision, nextLevel);
                }
                else if (alturaPlayer <= -65f)
                {
                    int nextLevel = actualIndexLevel + 3;
                    transitionLevel(collision, nextLevel);
                }
            }
            else if (actualLevel == "Fase-6.3")
            {
                int nextLevel = actualIndexLevel + 3;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-6.3.1")
            {
                int nextLevel = actualIndexLevel + 2;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-6.3.2")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
            else if (actualLevel == "Fase-6.4")
            {
                int nextLevel = actualIndexLevel + 1;
                transitionLevel(collision, nextLevel);
            }
        }
    }

    private void transitionLevel(Collider2D collision, int nextLevel)
    {
        //pausar juego y desactivar cualquier movimiento o accion del jugador
        Time.timeScale = 0f;
        Component[] playerScripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = false;
        }

        //desactivar la posibilidad de acceder al menu de pausa mientras transicionan las fases
        pauseMenu.SetActive(false);

        //pantalla de transicion
        transicionFases.SetActive(true);
        StartCoroutine(waitTransition(nextLevel));
    }

    IEnumerator waitTransition(int nextLevel)
    {
        transicionFases.transform.Find("Crossfade").GetComponent<Animator>().SetTrigger("DieTransition");
        yield return new WaitForSecondsRealtime(1.5f);

        transicionFases.transform.Find("Text").gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);

        //guardar partida
        SaveSystem.SavePlayer(player.GetComponent<PlayerHealth>());

        SaveSystem.buttonPressed = "levelTransition";
        SceneManager.LoadScene(nextLevel);
    }
}
