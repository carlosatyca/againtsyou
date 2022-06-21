using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolonchoCollider : MonoBehaviour
{
    public PlayerHealth player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.currentHealth > 0)
        {
            player.TakeDmg(player.currentHealth);
        }
    }
}
