using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheckNoFollow_Ent : MonoBehaviour
{

    private EnemyIA_Ent enemy;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_Ent>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !enemy.isAttacking && !enemy.cooldown)
        {
            enemy.Flip();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
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
            inRange = false;
            gameObject.SetActive(false);
            enemy.triggerArea.SetActive(true);
            enemy.inRange = false;
            enemy.SelectTarget();
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("isSitUp", false);
        }
    }
}


