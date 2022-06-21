using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotzoneCheck_AlmaErrante : MonoBehaviour
{
    private EnemyIA_AlmaErrante enemy;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyIA_AlmaErrante>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !enemy.isAttacking && !enemy.cooldown)
        {
            enemy.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
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
            animator.SetBool("Attack", false);
        }
    }

}
