using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase6_Subtitulos1 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "Siento que estoy cerca del final. Aguanta Enzo, ya casi estoy";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

