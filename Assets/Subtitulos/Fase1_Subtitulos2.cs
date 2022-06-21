using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase1_Subtitulos2 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "¿Que eran esos monstruos? ¿De verdad eran reales? Espero que Enzo este bien";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
