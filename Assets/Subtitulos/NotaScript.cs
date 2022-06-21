using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaScript : MonoBehaviour
{
    public static bool isTrigger = false;
    public string subtituloTexto = "";

    private void Awake()
    {
        GetComponent<AudioSource>().enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            isTrigger = true;
            subtituloTexto = "Papa, necesito ir a las catacumbas a investigar algo. Siento no haberte avisado. Firmado Enzo";
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(subtitulosCD(5f));
        }
    }
    IEnumerator subtitulosCD(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<AudioSource>().enabled = true;
    }

}
