using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_EsqueletoEspadaLarga : MonoBehaviour
{
    private EnemyIA_EsqueletoEspadaLarga enemy;
    private Animator animator;
    public PlayerHealth player;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_EsqueletoEspadaLarga>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is BoxCollider2D && animator.GetCurrentAnimatorStateInfo(0).IsName("EsqueletoEspadaLarga_Attack"))
        {   
            player.GetComponent<PlayerHealth>().TakeDmg(9f);
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
