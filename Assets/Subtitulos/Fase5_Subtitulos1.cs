using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase5_Subtitulos1 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "Bufff, por fin he llegado a las dichosas catacumbas. No debo perder mas tiempo, voy a buscar a mi hijo Enzo";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
