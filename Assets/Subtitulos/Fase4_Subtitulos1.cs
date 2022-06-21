using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase4_Subtitulos1 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "Ufff. No recordaba este sitio, ¿donde estan las catacumbas?";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
