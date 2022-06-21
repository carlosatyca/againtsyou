using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Ent : MonoBehaviour
{

    private EnemyIA_Ent enemy;
    private Animator animator;
    public PlayerHealth player;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Ent>();
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_NormalAttack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(4f);
        }
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("Ent_LowAttack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(3f);
        }
        else
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
        }
    }
}
