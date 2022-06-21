using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal2Trigger : MonoBehaviour
{
    public GameObject bossLimitRight;
    public static bool isTrigger = false;
    public string subtituloTexto = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossLimitRight.SetActive(true);
            isTrigger = true;
            subtituloTexto = "Esto tiene que ser una pesadilla, no me lo puedo creer";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

