using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaNoFollow : MonoBehaviour
{
    private EnemyIA_Calabazas enemy;
    private Animator animator;


    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Calabazas>();
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
            if(collision.transform.position.x < enemy.rightLimit.position.x && collision.transform.position.x > enemy.leftLimit.position.x){
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
            animator.SetBool("FastAttack", false);
            animator.SetBool("PotentialAttack", false);
            enemy.isAttacking = false;
        }
    }
}
