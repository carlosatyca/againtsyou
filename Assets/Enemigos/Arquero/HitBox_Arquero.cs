using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Arquero : MonoBehaviour
{
    private Animator animator;
    public PlayerHealth player;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.currentHealth > 0)
        {
            Destroy(gameObject);
            player.GetComponent<PlayerHealth>().TakeDmg(6f);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Attack", false);
        }
    }
}
