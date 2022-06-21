using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2_Subtitulos1 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "¿Ehhh? ¿Pero que ha pasado aqui? Todo esto antes era verde y ahora esta todo marchito y oscuro. Enzo... ¿donde estas?";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
