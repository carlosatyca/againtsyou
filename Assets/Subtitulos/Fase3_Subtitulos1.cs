using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase3_Subtitulos1 : MonoBehaviour
{
    public string subtituloTexto = "";
    public static bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            subtituloTexto = "Vaya... creo que este no era el camino hacia las catacumbas";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
