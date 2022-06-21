using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_EsqueletoEscudo : MonoBehaviour
{
    private EnemyIA_EsqueletoEscudo enemy;
    private Animator animator;
    public PlayerHealth player;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_EsqueletoEscudo>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoE_FastAttack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(7f);
        }
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoE_HardAttack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(8f);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoE_Shield"))
        {
            enemy.blockedHit = true;
        }
        else
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("Shield", false);
            enemy.blockedHit = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("Shield", false);
            enemy.blockedHit = false;
        }
    }
}
