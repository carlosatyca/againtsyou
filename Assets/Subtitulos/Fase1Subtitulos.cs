using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase1Subtitulos : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "Creo que el sol brilla mas de la cuenta... Me noto un poco oxidado, de camino al bosque, practicare un poco mis movimientos para sorprender a Enzo";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
