using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2_Subtitulos2 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "¿Eingg? No recordaba que el camino se bifurcase en varias direcciones. ¿Hacia donde habra ido mi hijo Enzo? Tendre que decidir por donde avanzar";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
