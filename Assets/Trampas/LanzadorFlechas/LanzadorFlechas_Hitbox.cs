using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorFlechas_Hitbox : MonoBehaviour
{
    public PlayerHealth player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.currentHealth > 0)
        {
            if(collision is BoxCollider2D)
            {
                player.GetComponent<PlayerHealth>().TakeDmg(5f);
                if(collision is CircleCollider2D)
                {
                  //  
                }
            }
            if (collision is CircleCollider2D)
            {
                player.GetComponent<PlayerHealth>().TakeDmg(5f);
                if (collision is BoxCollider2D)
                {
                    //
                }
            }
        }
    }
}
