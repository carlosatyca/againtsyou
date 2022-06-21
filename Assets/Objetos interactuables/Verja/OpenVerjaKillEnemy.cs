using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenVerjaKillEnemy : MonoBehaviour
{
    public GameObject enemigos;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Fase-6.1")
        {
            if (gameObject.name == "Verja2" && enemigos.transform.Find("EsqueletoLanza").Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0)
            {
                gameObject.GetComponent<Verja>().OpenVerja();
            }

            else if (gameObject.name == "Verja" && enemigos.transform.Find("Arquero").Find("Arquero").GetComponent<EnemyIA_Arquero>().currentHealth <= 0 &&
                enemigos.transform.Find("EsqueletoLanza1").Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0)
            {
                gameObject.GetComponent<Verja>().OpenVerja();
            }
            else if (gameObject.name == "Verja1" && enemigos.transform.Find("Arquero2").Find("Arquero").GetComponent<EnemyIA_Arquero>().currentHealth <= 0 &&
                 enemigos.transform.Find("EsqueletoLanza2").Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0 &&
                 enemigos.transform.Find("EsqueletoLanza3").Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0)
            {
                gameObject.GetComponent<Verja>().OpenVerja();
            }
        } 
        else if (SceneManager.GetActiveScene().name == "Fase-6.2")
        {
            if (gameObject.name == "Verja" && enemigos.transform.Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0)
            {
                gameObject.GetComponent<Verja>().OpenVerja();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Fase-6.3.2")
        {
            if (gameObject.name == "Verja" && enemigos.transform.Find("EsqueletoLanza").Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0 &&
                enemigos.transform.Find("EsqueletoLanza1").Find("EsqueletoLanza").GetComponent<EnemyIA>().currentHealth <= 0)
            {
                gameObject.GetComponent<Verja>().OpenVerja();
            }
        }
    }
}
