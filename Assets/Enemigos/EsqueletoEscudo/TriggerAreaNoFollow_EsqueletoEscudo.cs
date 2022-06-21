using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaNoFollow_EsqueletoEscudo : MonoBehaviour
{
    private EnemyIA_EsqueletoEscudo enemy;
    private Animator animator;


    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_EsqueletoEscudo>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (enemy.inRange && !enemy.isAttacking && !enemy.cooldown)
        {
            enemy.Flip();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < enemy.rightLimit.position.x && collision.transform.position.x > enemy.leftLimit.position.x)
            {
                enemy.target = collision.transform;
                enemy.inRange = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.inRange = false;
            enemy.SelectTarget();
            animator.SetBool("EsqueletoE_FastAttack", false);
            animator.SetBool("EsqueletoE_HardAttack", false);
            animator.SetBool("EsqueletoE_Shield", false);
            enemy.isAttacking = false;
        }
    }
}
