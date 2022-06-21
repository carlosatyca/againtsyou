using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Calabazas : MonoBehaviour
{
    private EnemyIA_Calabazas enemy;
    private Animator animator;
    public PlayerHealth player;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Calabazas>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("Calabazas_FastAttack"))
        {   
            player.GetComponent<PlayerHealth>().TakeDmg(4f);
        }
        else if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("Calabazas_PotentialAttack"))
        {
            player.GetComponent<PlayerHealth>().TakeDmg(5f);
        }
        else
        {
            animator.SetBool("FastAttack", false);
            animator.SetBool("PotentialAttack", false);
        }
 

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("FastAttack", false);
            animator.SetBool("PotentialAttack", false);
        }
    }
}
