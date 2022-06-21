using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedToStatic : MonoBehaviour
{
    public GameObject pared;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deslizable"))
        {
            pared.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
