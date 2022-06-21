using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal1Trigger : MonoBehaviour
{
    public GameObject bossLimitLeft;
    public GameObject bossLimitRight;
    public static bool isTrigger = false;
    public string subtituloTexto = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossLimitLeft.SetActive(true);
            bossLimitRight.SetActive(true);
            isTrigger = true;
            subtituloTexto = "Pero ¿Que hago yo ahi? ¿Esto es real? ¿Donde esta mi hijo?";
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

